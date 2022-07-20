# Agregar migracion
add-migration Inicial -Context ComercioElectronicoDbContext

# Aplicar migracion
Update-Database -Context ComercioElectronicoDbContext 

# Realizar migracion por script
Script-Migration -Context ComercioElectronicoDbContext -From AddConfigurationUpdate

# Genera script desde la primera migracion hasta la ultima
Script-Migration -Context ComercioElectronicoDbContext 0

# Eliminar Migracion
remove-migration
