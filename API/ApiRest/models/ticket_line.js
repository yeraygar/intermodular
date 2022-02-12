const mongoose = require("mongoose");

const ticketLineSchema = mongoose.Schema({
    name:{
        type:String,
        required:true
    },
    cantidad:{
        type: Number,
        required:false,
        default: 1
    },
    precio:{
        type: Number,
        required: false,
        default: 0.0
    },
    stock:{
        type: Number,
        required: false,
        default: 1
    },
    total:{
        type: Number,
        required: false,
        default: 0.0 // cantidad * precio
    },
    id_client:{
        type: String,
        required: true
    },
    id_familia:{
        type:String,
        required: true,
    },
    id_ticket:{
        type:String,
        required: false,
        default: "Error"
    },
    comentario:{
        type: String,
        required: false,
        default: ""
    }

})
module.exports = mongoose.model('TicketLine', ticketLineSchema);