package inter.intermodular.models

/**
 * Modelo usado para mandar
 * nuevos tickets a la API
 */
data class TicketPost (
    var id_user_que_abrio : String,
    var id_client : String,
    var id_table : String,
    var name_table : String,
)