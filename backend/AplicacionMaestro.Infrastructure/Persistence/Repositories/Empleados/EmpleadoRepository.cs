using AplicacionMaestro.Application.Interfaces.Empleados;
using AplicacionMaestro.Domain.Entities;
using AplicacionMaestro.Infrastructure.Persistence.DbContext;
using AplicacionMaestro.Infrastructure.Persistence.Entities.Empleados;
using Microsoft.EntityFrameworkCore;

namespace AplicacionMaestro.Infrastructure.Persistence.Repositories.Empleados
{

    public class EmpleadoRepository : IEmpleadoRepository
    {
        private readonly ApplicationDbContext _context;

        public EmpleadoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SincronizarAsync(
            List<Empleado> empleados,
            string usuario,
            CancellationToken ct)
        {
            if (empleados == null || empleados.Count == 0)
                return;

            _context.ChangeTracker.AutoDetectChangesEnabled = false;

            try
            {
                // ========================================
                // 1 Obtener IDs externos
                // ========================================
                var externalIds = empleados
                    .Select(x => x.IdEmpleadoExternal)
                    .ToHashSet();

                // ========================================
                // 2 Traer empleados existentes
                // ========================================
                var existentes = await _context.Empleados
                    .Where(x => externalIds.Contains(x.IdEmpleadoExternal))
                    .ToListAsync(ct);

                var empleadosDict = existentes
                    .ToDictionary(x => x.IdEmpleadoExternal);

                var nuevos = new List<EmpleadoEntity>();

                // ========================================
                // 3 Sincronización
                // ========================================
                foreach (var empleado in empleados)
                {
                    if (!empleadosDict.TryGetValue(empleado.IdEmpleadoExternal, out var entity))
                    {
                        entity = CrearEntity(empleado, usuario);
                        nuevos.Add(entity);
                    }
                    else
                    {
                        ActualizarEntity(entity, empleado, usuario);
                    }
                }

                // ========================================
                // 4 Insertar nuevos
                // ========================================
                if (nuevos.Count > 0)
                    await _context.Empleados.AddRangeAsync(nuevos, ct);

            }
            finally
            {
                _context.ChangeTracker.AutoDetectChangesEnabled = true;
            }
        }

        #region Métodos de mapeo
        // ========================================
        // Crear entidad
        // ========================================
        private EmpleadoEntity CrearEntity(
            Empleado empleado,
            string usuario)
        {
            return new EmpleadoEntity
            {
                IdEmpleadoExternal = empleado.IdEmpleadoExternal,
                Codigo = empleado.Codigo,
                NombreCompleto = empleado.NombreCompleto,

                Apellido = empleado.Principal.Apellido,
                Nombre = empleado.Principal.Nombre,
                Alias = empleado.Principal.Alias,
                Cargo = empleado.Principal.Cargo,
                Departamento = empleado.Principal.Departamento,
                Estado = empleado.Principal.Estado,
                Turno = empleado.Principal.Turno,
                Categoria = empleado.Principal.Categoria,
                IdUsuario = empleado.Principal.IdUsuario,
                FrecuenciaPago = empleado.Principal.FrecuenciaPago,
                TipoEmpleado = empleado.Principal.TipoEmpleado,
                GeneraNomina = empleado.Principal.GeneraNomina,
                CuentaSueldo = empleado.Principal.CuentaSueldo,

                PrimerNombre = empleado.NameTax.PrimerNombre,
                SegundoNombre = empleado.NameTax.SegundoNombre,
                PrimerApellido = empleado.NameTax.PrimerApellido,
                SegundoApellido = empleado.NameTax.SegundoApellido,

                Direccion1 = empleado.Contacto.Direccion1,
                Direccion2 = empleado.Contacto.Direccion2,
                Direccion3 = empleado.Contacto.Direccion3,
                Direccion4 = empleado.Contacto.Direccion4,
                Ciudad = empleado.Contacto.Ciudad,
                CodProvincia = empleado.Contacto.Provincia,
                CP = empleado.Contacto.CodigoPostal,
                Municipio = empleado.Contacto.Municipio,
                Telefono = empleado.Contacto.Telefono,
                TelComercial = empleado.Contacto.TelefonoComercial,
                ExtensionTel = empleado.Contacto.ExtensionTelefono,
                CorreoElect = empleado.Contacto.CorreoElectronico,
                Correo = empleado.Contacto.Correo,

                FechaContratacion = empleado.RecursosHumanos.FechaContratacion,
                FechaRevision = empleado.RecursosHumanos.FechaRevision,
                FechaRescision = empleado.RecursosHumanos.FechaRescision,

                UsuarioRegistro = usuario,
                FechaCreacion = DateTime.UtcNow
            };
        }

        // ========================================
        // Actualizar entidad
        // ========================================
        private void ActualizarEntity(
            EmpleadoEntity entity,
            Empleado empleado,
            string usuario)
        {
            entity.Codigo = empleado.Codigo;
            entity.NombreCompleto = empleado.NombreCompleto;

            entity.Apellido = empleado.Principal.Apellido;
            entity.Nombre = empleado.Principal.Nombre;
            entity.Alias = empleado.Principal.Alias;
            entity.Cargo = empleado.Principal.Cargo;
            entity.Departamento = empleado.Principal.Departamento;
            entity.Estado = empleado.Principal.Estado;
            entity.Turno = empleado.Principal.Turno;
            entity.Categoria = empleado.Principal.Categoria;
            entity.IdUsuario = empleado.Principal.IdUsuario;
            entity.FrecuenciaPago = empleado.Principal.FrecuenciaPago;
            entity.TipoEmpleado = empleado.Principal.TipoEmpleado;
            entity.GeneraNomina = empleado.Principal.GeneraNomina;
            entity.CuentaSueldo = empleado.Principal.CuentaSueldo;

            entity.PrimerNombre = empleado.NameTax.PrimerNombre;
            entity.SegundoNombre = empleado.NameTax.SegundoNombre;
            entity.PrimerApellido = empleado.NameTax.PrimerApellido;
            entity.SegundoApellido = empleado.NameTax.SegundoApellido;

            entity.Direccion1 = empleado.Contacto.Direccion1;
            entity.Direccion2 = empleado.Contacto.Direccion2;
            entity.Direccion3 = empleado.Contacto.Direccion3;
            entity.Direccion4 = empleado.Contacto.Direccion4;
            entity.Ciudad = empleado.Contacto.Ciudad;
            entity.CodProvincia = empleado.Contacto.Provincia;
            entity.CP = empleado.Contacto.CodigoPostal;
            entity.Municipio = empleado.Contacto.Municipio;
            entity.Telefono = empleado.Contacto.Telefono;
            entity.TelComercial = empleado.Contacto.TelefonoComercial;
            entity.ExtensionTel = empleado.Contacto.ExtensionTelefono;
            entity.CorreoElect = empleado.Contacto.CorreoElectronico;
            entity.Correo = empleado.Contacto.Correo;

            entity.FechaContratacion = empleado.RecursosHumanos.FechaContratacion;
            entity.FechaRevision = empleado.RecursosHumanos.FechaRevision;
            entity.FechaRescision = empleado.RecursosHumanos.FechaRescision;

            entity.UsuarioModificacion = usuario;
            entity.FechaModificacion = DateTime.UtcNow;
        }

        #endregion
    }
}
