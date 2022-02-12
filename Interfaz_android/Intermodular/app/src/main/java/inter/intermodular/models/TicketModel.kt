package inter.intermodular.models

import java.util.*

/**
 * Modelo usado para recibir
 * las respuestas de la API
 */
data class TicketModel(
    var _id : String,
    var total : Float,
    var tipo_ticket : String,
    var id_user_que_abrio : String,
    var id_user_que_cerro: String,
    var id_client : String,
    var id_table : String,
    var name_table : String,
    var comensales : Int,
    var date : Date,
    var cobrado: Boolean,
)