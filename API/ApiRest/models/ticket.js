const mongoose = require("mongoose");

const ticketSchema = mongoose.Schema({
    total:{
        type: Number,
        required: false,
        default: 0.0
    },
    tipo_ticket:{ //Efectivo o Tarjeta
        type:String,
        required: true 
    },
    id_user:{
        type: String,
        required: true,
    },
    id_client:{
        type: String,
        required: true
    },
    name_table:{
        type: String,
        required: true
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
        required: true,
        default: false,
    }
})
module.exports = mongoose.model('Ticket', ticketSchema);