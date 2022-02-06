package inter.intermodular.models

/**
 * Modelo usado para recibir
 * las respuestas de la API
 */
data class TableModel(
    var _id: String,
    var name: String,
    var status: Boolean,
    var id_zone: String,
    var comensales: Int,
    var num_row: Int,
    var num_column: Int,
    var comensalesMax: Int,
    var id_user: String
)
