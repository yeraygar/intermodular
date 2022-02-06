package inter.intermodular.models

/**
 * Modelo usado para recibir
 * las respuestas de la API
 */
data class ZoneModel(
    var _id: String,
    var zone_name: String,
    var id_client: String,
)
