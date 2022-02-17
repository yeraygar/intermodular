const express = require("express");
const router = express.Router();
const ticketLineSchema = require("../models/ticket_line");


//Create new ticket_line
router.post("/ticket_line", (req, res) => {
    const ticket_line = ticketLineSchema(req.body);
    ticket_line
        .save()
        .then((data) =>{
            res.json(data);
            console.log(`\nNew ticket_line: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.error(`Error get /api/ticket_line : ${err}`);
        })
})

//Get specific ticket_line
router.get("/ticket_line/:id", (req, res) => {
    const {id} = req.params;
    ticketLineSchema
        .findById(id)
        .then((data) =>{
            res.json(data);
            console.log(`\nticket_line: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.log(`Error get /api/ticket_line/${id} : ${err}`);
        })
})

//Update ticket_line, si algun campo no se pone no se elimina
router.put("/ticket_line/:id", (req, res) => {
    const {id} = req.params;
    const {name, cantidad, precio, stock, total, id_client, id_familia, id_ticket, comentario} = req.body;
    ticketLineSchema
        .updateOne({_id: id}, {$set:{name, cantidad, precio, stock, total, id_client, id_familia, id_ticket, comentario}})
        .then((data) =>{
            res.json(data);
            console.log(`\nticket_line Update succesful: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.log(`ticket_line Error update : ${err}`);
        })
})

//Delete ticket_line
router.delete("/ticket_line/:id", (req, res) => { 
    const {id} = req.params;
    ticketLineSchema
        .remove({_id: id})
        .then((data) =>{
            res.json(data);
            console.log(`\nticket_line Delete succesful: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.log(`ticket_line Error delete : ${err}`);
        })
})

//Get all ticket_line from a Ticket
router.get("/ticket_line/ticket/:id_ticket", (req, res) => {
    const {id_ticket} = req.params;
    ticketLineSchema
        .find({id_ticket: id_ticket})
        .then((data) =>{
            res.json(data);
            console.log(`\nticket_line: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.log(`Error get /api/ticket_line/ticket/${id} : ${err}`);
        })
})

//Delete all ticket_line from a Ticket
router.delete("/ticket_line/ticket/:id_ticket", (req, res) => {
    const {id_ticket} = req.params;
    ticketLineSchema
    .deleteMany({id_ticket : id_ticket})
    .then((data) =>{
        res.json(data);
        console.log("Todas la lineas del ticket fueron eliminadas "+ data);
    })
    .catch((error) => {
        res.json({message:error});
        console.log("Error al borrar las líneas de pedido " +error);
    })
})

module.exports = router