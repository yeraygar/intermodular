package inter.intermodular.models

/**
 * Modelo usado para recibir
 * las respuestas de la API
 */
data class ProductModel(
    var _id: String,
    var name: String,
    var cantidad: Int,
    var precio: Float,
    var stock: Int,
    var total: Float,
    var id_client: String,
    var id_familia: String,
    var id_ticket: String,
    val comentario: String
)