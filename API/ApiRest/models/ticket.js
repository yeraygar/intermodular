const mongoose = require("mongoose");

const ticketSchema = mongoose.Schema({
    total:{
        type: Number,
        required: false,
        default: 0.0
    },
    tipo_ticket:{ //Efectivo o Tarjeta
        type:String,
        required: false,
        defafult: "Efectivo"
    },
    id_user_que_abrio:{ 
        type: String,
        required: true,
        default: "Error"
    },
    id_user_que_cerro:{
        type: String,
        required: false,
        default: "Error"
    },
    id_client:{
        type: String,
        required: true,
        default: "Error"
    },
    id_table:{
        type: String,
        required: true,
        default: "Error"
    },
    id_caja:{
        type:String,
        required: false,
        default: "Error"
    },
    name_table:{
        type: String,
        required: true,
        default: "Error"
    },
    comensales:{
        type: Number,
        required: false,
        default: 1
    },
    date:{
        type: Date,
        required: false,
        default: Date.now
    },
    cobrado:{
        type: Boolean,
        required: false,
        default: false,
    }
})
module.exports = mongoose.model('Ticket', ticketSchema);