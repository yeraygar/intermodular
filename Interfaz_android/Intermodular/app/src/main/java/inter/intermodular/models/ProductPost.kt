package inter.intermodular.models

/**
 * Modelo usado para mandar
 * nuevos Productos a la API
 */
data class ProductPost(
    var name : String,
    var precio : Float,
    var cantidad : Int,
    var total : Float,
    var id_client : String,
    var id_familia : String,
    var id_ticket : String
)