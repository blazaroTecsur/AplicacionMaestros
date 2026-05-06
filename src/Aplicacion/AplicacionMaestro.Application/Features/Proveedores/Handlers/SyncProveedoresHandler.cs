using AplicacionMaestro.Application.Features.Proveedores.Models;
using AplicacionMaestro.Application.Interfaces;
using AplicacionMaestro.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AplicacionMaestro.Application;

public class SyncProveedoresHandler
    : IRequestHandler<SyncProveedoresCommand, Unit>
{
    private readonly IProveedorRepository _proveedorRepository;          // BD NUEVA
    private readonly IProveedorLegacyRepository _proveedorLegacyRepository; // BD LEGACY
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<SyncProveedoresHandler> _logger;

    public SyncProveedoresHandler(
        IProveedorRepository proveedorRepository,
        IProveedorLegacyRepository proveedorLegacyRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        ILogger<SyncProveedoresHandler> logger)
    {
        _proveedorRepository = proveedorRepository;
        _proveedorLegacyRepository = proveedorLegacyRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Unit> Handle(
           SyncProveedoresCommand request,
           CancellationToken cancellationToken)
    {
        _logger.LogInformation("Inicio de sincronización de proveedores. Total={total}",
            request.Proveedores?.Count ?? 0);

        await _unitOfWork.BeginTransactionAsync(cancellationToken);

        var legacyList = new List<ProveedorLegacySyncModel>();

        try
        {
            var proveedoresDomain = _mapper
                .Map<List<Proveedor>>(request.Proveedores);

            await _proveedorRepository.SincronizarAsync(
                proveedoresDomain,
                "SyncProveedoresHandler",
                cancellationToken);

            legacyList.AddRange(MapToLegacy(request.Proveedores));

            await _unitOfWork.CommitAsync(cancellationToken);

            _logger.LogInformation(
                "Fin sincronización de proveedores. Procesados={total}",
                proveedoresDomain.Count);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync(cancellationToken);

            _logger.LogError(ex, "Error crítico en sincronización proveedores");

            throw;
        }
        try
        {
            // 🔥 Fase 2 - Legacy (fuera de la transacción)
            await _proveedorLegacyRepository.SincronizarAsync(legacyList, cancellationToken);

            _logger.LogInformation("Fin sincronización legacy de proveedores.");

        }
        catch (Exception ex)
        {
            _logger.LogError(
               ex,
               "Error inesperado sincronizando proveedores legacy");
        }
        _logger.LogInformation("Fin sincronización proveedores OK");

        return Unit.Value;
    }

    #region Metodos privados

    private List<ProveedorLegacySyncModel> MapToLegacy(
        IEnumerable<ProveedorSyncDto> ldto)
    {
        return ldto.Select(dto => new ProveedorLegacySyncModel
        {
            IdExternal = dto.IdProveedor,
            Ruc = dto.Imps.Ruc,
            RazonSocial = dto.Proveedor,
            TipoPersona = dto.TipoPersona,

            Direccion1 = dto.Direccion1,
            Direccion2 = dto.Direccion2,
            Direccion3 = dto.Direccion3,
            Direccion4 = dto.Direccion4,

            Comprador = dto.Comprador,

            ContactoNombre = dto.Contacto?.Contacto,
            ContactoTelefono = dto.Contacto?.Telefono,
            CorreoExt = dto.Contacto?.CorreoExt,
            CorreoInt = dto.Contacto?.CorreoInt,

            Estado = dto.Estado
        }).ToList();
    }

    #endregion

}
