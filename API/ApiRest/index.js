/**
 * Para poder utilizar el archivo request.http 
 * hay que instalar plugin REST Client
 * 
 * Para instalar todas las dependencias usar
 * npm install desde dentro del proyecto (donde
 * esta index.js) Usar npm start para arrancar
 */

//REQUIREDS Y CONSTANTS
const fs = require('fs');
const path = require('path');
const port = process.env.PORT || 8081;
const mongoose = require("mongoose");
const userRoutes = require("./routes/user");
const tableRoutes = require("./routes/table");
const zoneRoutes = require("./routes/zone");
const express = require('express');
const app = express();
require("dotenv").config();

//MIDDLEWARE's
app.use(express.urlencoded({extended:false}));
app.use(express.json());
app.use('/api', tableRoutes);
app.use('/api', userRoutes);
app.use("/api", zoneRoutes);

//CONNECTION WITH MONGODB
mongoose
.connect(process.env.MONGODB_URI)
//.connect("mongodb://127.0.0.1:27017/myFirstDatabase")
.then(() => console.log("Connected to MongoDB Atlas"))
.catch((err) => console.log(`Error al conectar con MongoDB: ${err}`));

//INIT ROUTE (Dev-Test)
app.get("/inicio", (req, res) =>
  res.sendFile(path.join(__dirname, '/www/inicio.html')));
app.get("/", (req, res) =>
  res.sendFile(path.join(__dirname, '/www/inicio.html')));

//FAVICON
const favicon = require('serve-favicon');
app.get("/favicon.ico", () => 
  app.use(favicon(path.join(__dirname, '/www/favicon.ico'))));

//INIT SERVER
app.listen(port, () => console.log(`Server running on port ${port}`));

