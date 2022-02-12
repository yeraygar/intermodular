package inter.intermodular.models

/**
 * Modelo usado para mandar
 * nuevas mesas a la API
 */
data class TablePost(
    var name: String,
    var id_zone: String,
    var num_row: Int,
    var num_column: Int,
    var comensalesMax: Int,
    var id_user: String
)
