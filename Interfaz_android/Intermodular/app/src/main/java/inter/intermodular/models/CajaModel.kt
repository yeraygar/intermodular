package inter.intermodular.models

import java.util.*

/**
 * Modelo usado para recibir
 * las respuestas de la API
 */
data class CajaModel (
    var _id : String,
    var fecha_apertura: Date,
    var fecha_cierre: Date,
    var cerrada : Boolean,
    var total : Float,
    var id_client : String
    )