package inter.intermodular.models

/**
 * Modelo usado para mandar
 * nuevas Zonas a la API
 */
data class ZonePost(
    var zone_name: String,
    var id_client: String,
)
