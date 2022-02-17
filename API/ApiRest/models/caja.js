const mongoose = require("mongoose")
const cajaSchema = mongoose.Schema({
    fecha_apertura:{
        type: Date,
        required: false,
        default: Date.now
    },
    fecha_cierre:{
        type: Date,
        required: false,
        default: Date.now
    },
    cerrada:{
        type: Boolean,
        required: false,
        default: false
    },
    total:{
        type: Number,
        required: false,
        default: 0
    },
    id_client:{
        type: String,
        required: true,
        default: "Error"
    }
})
module.exports = mongoose.model('Caja', cajaSchema);