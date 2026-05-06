# Estrategia de Pruebas

Este documento describe los casos de prueba implementados para el módulo de sincronización de tablas maestras.

---

# Arquitectura de Testing

Las pruebas unitarias utilizan:

- xUnit
- Assert
- EF Core InMemory (para pruebas de persistencia)

---

# SyncAptitudesHandler

Este handler sincroniza aptitudes provenientes de un sistema externo.

## Casos cubiertos

### CASO 1 — Insertar nuevas

Descripción:
Si un registro no existe en la base de datos debe insertarse.

Resultado esperado:
Se insertan nuevos registros en la tabla Maestro.

---

### CASO 2 — Actualización con cambios

Descripción:
Si el registro existe pero su descripción cambió debe actualizarse.

Resultado esperado:
El registro se actualiza.

---

### CASO 3 — Sin cambios

Descripción:
Si los datos recibidos son iguales a los existentes no debe ejecutarse actualización.

Resultado esperado:
No se modifican registros.

---

### CASO 4 — Cambio de estado (NO APLICA)

Descripción:
Si un registro cambia de Activo a Inactivo se debe actualizar.

Resultado esperado:
El estado del registro cambia.

---

### CASO 5 — Regla de dominio 

Descripción:
No se permite registrar con código vacío.

Resultado esperado:
Se lanza una excepción de dominio.

---

### CASO 6 — Mezcla Insert / Update

Descripción:
Cuando una trama contiene registros nuevos y existentes.

Resultado esperado:
Se insertan los nuevos y se actualizan los modificados.

---

### CASO 7 — Idempotencia

Descripción:
Si la misma trama se ejecuta dos veces no deben generarse duplicados.

Resultado esperado:
La base de datos mantiene un único registro por código.

---

### CASO 8 — Duplicados en la trama

Descripción:
Si la misma trama contiene códigos duplicados.

Resultado esperado:
Debe evitar duplicados.

---

### CASO 9 — Trama vacía	

Descripción:
Si la trama viene vacía.

Resultado esperado:
El handler no debe hacer nada.

---

# Cobertura esperada

- Dominio
- Application
- Persistencia (InMemory)

Cobertura mínima recomendada: **80%**