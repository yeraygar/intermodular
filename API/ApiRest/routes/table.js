const express = require("express");
const router = express.Router();
const tableSchema = require("../models/table");

//Create new Table
router.post("/tables", (req, res) => {
    const table = tableSchema(req.body);
    table
        .save()
        .then((data) =>{
            res.json(data);
            console.log(`\nNew Table: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.error(`Error get /api/table : ${err}`);
        })
})

//Get all Tables
router.get("/tables", (req, res) => {
    tableSchema
        .find()
        .then((data) =>{
            res.json(data);
            console.log(`\nAll Tables: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.log(`Error get /api/tables : ${err}`);
        })
})

//Get specific table
router.get("/tables/:id", (req, res) => {
    const {id} = req.params;
    tableSchema
        .findById(id)
        .then((data) =>{
            res.json(data);
            console.log(`\nTable: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.log(`Error get /api/tables/${id} : ${err}`);
        })
})

//Update table, si algun campo no se pone no se elimina
router.put("/tables/:id", (req, res) => {
    const {id} = req.params;
    const {name, status, id_client, id_zone, id_row, id_column, comensales, id_user} = req.body;
    tableSchema
        .updateOne({_id: id}, {$set:{name, status, id_client, id_zone, id_row, id_column, comensales, id_user}})
        .then((data) =>{
            res.json(data);
            console.log(`\nUpdate succesful: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.log(`Error update : ${err}`);
        })
})

//Delete User
router.delete("/tables/:id", (req, res) => {
    const {id} = req.params;
    tableSchema
        .remove({_id: id})
        .then((data) =>{
            res.json(data);
            console.log(`\nDelete succesful: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.log(`Error delete : ${err}`);
        })
})

module.exports = router;