package inter.intermodular.models

/**
 * Modelo usado para mandar
 * nuevos Productos a la API
 */
data class ProductPost(
    var precio : Float,
    var stock : Int,
    var id_client : String,
    var id_familia : String,
)