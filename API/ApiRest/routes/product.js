const express = require("express");
const router = express.Router();
const productSchema = require("../models/product");

//Create new product
router.post("/product", (req, res) => {
    const product = productSchema(req.body);
    product
        .save()
        .then((data) =>{
            res.json(data);
            console.log(`\nNew product: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.error(`Error get /api/product : ${err}`);
        })
})

//Get specific product
router.get("/product/:id", (req, res) => {
    const {id} = req.params;
    productSchema
        .findById(id)
        .then((data) =>{
            res.json(data);
            console.log(`\nproduct: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.log(`Error get /api/product/${id} : ${err}`);
        })
})

//Update product, si algun campo no se pone no se elimina
router.put("/product/:id", (req, res) => {
    const {id} = req.params;
    const {name, email, active} = req.body;
    productSchema
        .updateOne({_id: id}, {$set:{name, email, active}})
        .then((data) =>{
            res.json(data);
            console.log(`\nproduct Update succesful: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.log(`product Error update : ${err}`);
        })
})

//Delete product
router.delete("/product/:id", (req, res) => { 
    const {id} = req.params;
    productSchema
        .remove({_id: id})
        .then((data) =>{
            res.json(data);
            console.log(`\nproduct Delete succesful: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.log(`product Error delete : ${err}`);
        })
})

//Get all product from a client
router.get("/product/client/:id_client", (req, res) => {
    const {id_client} = req.params;
    productSchema
        .find({id_client: id_client})
        .then((data) =>{
            res.json(data);
            console.log(`\nproduct: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.log(`Error get /api/product/client/${id} : ${err}`);
        })
})

//Get all product from a family
router.get("/product/family/:id_familia", (req, res) => {
    const {id_familia} = req.params;
    productSchema
        .find({id_familia: id_familia})
        .then((data) =>{
            res.json(data);
            console.log(`\nproduct: \n ${data}`);
        })
        .catch((err) => {
            res.json({message:err});
            console.log(`Error get /api/product/client/${id} : ${err}`);
        })
})

module.exports = router