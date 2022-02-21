package inter.intermodular.screens.table_payment

import android.content.Context
import android.widget.Toast
import androidx.compose.foundation.ExperimentalFoundationApi
import androidx.compose.foundation.clickable
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.GridCells
import androidx.compose.foundation.lazy.LazyVerticalGrid
import androidx.compose.foundation.shape.RoundedCornerShape
import androidx.compose.foundation.text.KeyboardOptions
import androidx.compose.material.*
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Calculate
import androidx.compose.runtime.Composable
import androidx.compose.runtime.MutableState
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.res.colorResource
import androidx.compose.ui.text.TextStyle
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.text.input.KeyboardType
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import androidx.compose.ui.window.Dialog
import androidx.navigation.NavController
import inter.intermodular.R
import inter.intermodular.ScreenNav
import inter.intermodular.models.ProductModel
import inter.intermodular.support.currentTable
import inter.intermodular.support.currentTicket
import inter.intermodular.support.currentUser
import inter.intermodular.support.firstOpenTable
import inter.intermodular.view_models.TableViewModel
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.launch
import java.lang.Exception
import java.util.*

@OptIn(ExperimentalFoundationApi::class)
@Composable
fun ShowAlertDialogCobrar(
    isCobrarOpen: MutableState<Boolean>,
    tableViewModel: TableViewModel,
    applicationContext: Context,
    navController: NavController,
    scaffoldState: ScaffoldState,
    scope: CoroutineScope,
    currentTicketLines: MutableState<List<ProductModel>>
) {
    var aceptarEnabled = remember { mutableStateOf(false) }
    var cashOk = remember { mutableStateOf(false) }
    var pagoTarjeta = remember { mutableStateOf(false) }
    var cashInput = remember { mutableStateOf(0f) }

    cashOk.value = (cashInput.value >= currentTicket.total)
    if (pagoTarjeta.value) aceptarEnabled.value = true
    else aceptarEnabled.value = cashOk.value

    Dialog(onDismissRequest = { isCobrarOpen.value = false }) {
        Surface(
            modifier = Modifier
                .padding(10.dp),
            shape = RoundedCornerShape(5.dp),
            color = Color.White
        ) {
            Column(
                horizontalAlignment = Alignment.CenterHorizontally,
                verticalArrangement = Arrangement.Center,
                modifier = Modifier
                    .padding(10.dp)
            ) {

                Card(
                    backgroundColor = colorResource(id = R.color.gris_muy_claro),
                    modifier = Modifier
                        .fillMaxWidth()
                        .padding(10.dp)
                ) {
                    Text(
                        text = "TOTAL MESA ${currentTable.name} :  ${currentTicket.total}€" ,
                        fontWeight = FontWeight.Bold,
                        fontSize = 16.sp,
                        modifier = Modifier.padding(10.dp))

                }

                Spacer(modifier = Modifier.height(5.dp))

                LazyVerticalGrid(cells = GridCells.Fixed(3), modifier = Modifier.fillMaxWidth(), contentPadding = PaddingValues(10.dp)){
                    item{
                        Card(
                            backgroundColor = if (pagoTarjeta.value) Color.White else colorResource(id = R.color.azul),
                            modifier = Modifier
                                .fillMaxWidth()
                                .height(50.dp)
                                .clickable { pagoTarjeta.value = false }
                        ){
                            Column (
                                horizontalAlignment = Alignment.CenterHorizontally,
                                verticalArrangement = Arrangement.Center,
                                modifier = Modifier.fillMaxWidth()
                            ){
                                Text(
                                    text = "EFECTIVO",
                                    modifier = Modifier.padding(5.dp),
                                    fontWeight = FontWeight.Bold,
                                    color = if (pagoTarjeta.value) Color.Black else Color.White
                                )
                            }
                        }
                    }
                    item{
                        Switch(
                            checked = pagoTarjeta.value,
                            onCheckedChange = { pagoTarjeta.value = it},
                        )
                    }
                    item{
                        Card(
                            backgroundColor = if (!pagoTarjeta.value) Color.White else colorResource(id = R.color.azul),
                            modifier = Modifier
                                .fillMaxWidth()
                                .height(50.dp)
                                .clickable { pagoTarjeta.value = true }
                        ){
                            Column (
                                horizontalAlignment = Alignment.CenterHorizontally,
                                verticalArrangement = Arrangement.Center,
                                modifier = Modifier.fillMaxWidth()
                            ){
                                Text(
                                    text = "TARJETA",
                                    modifier = Modifier.padding(5.dp),
                                    fontWeight = FontWeight.Bold,
                                    color = if (!pagoTarjeta.value) Color.Black else Color.White
                                )
                            }
                        }
                    }
                }

                OutlinedTextField(
                    enabled = !pagoTarjeta.value,
                    value = cashInput.value.toString(),
                    onValueChange = { it.also {
                        try{
                            cashInput.value = it.toFloat()

                        }catch (e: Exception){
                            Toast.makeText(applicationContext, "Caracteres Invalidos", Toast.LENGTH_SHORT).show()
                        }
                    } },
                    label = { Text(text = "Cantidad Introducida", style = TextStyle(
                        color = colorResource(id = R.color.gris_claro)
                    ),) },
                    placeholder = { Text(text = "Cantidad Introducida",style = TextStyle(
                        color = colorResource(id = R.color.gris_claro)
                    ),) },
                    singleLine = true,
                    keyboardOptions = KeyboardOptions(keyboardType = KeyboardType.Number),
                    trailingIcon = {
                        val image = Icons.Filled.Calculate
                        IconButton(onClick = { cashInput.value = currentTicket.total }
                        ) { Icon(imageVector  = image, "Dinero") }
                    },
                    colors = TextFieldDefaults.outlinedTextFieldColors(
                        focusedBorderColor = colorResource(id = R.color.azul_oscuro),
                        unfocusedBorderColor = colorResource(id = R.color.gris_claro),
                        focusedLabelColor = colorResource(id = R.color.azul_oscuro),
                        unfocusedLabelColor = colorResource(id = R.color.gris_claro),
                        cursorColor = colorResource(id = R.color.azul)
                    ),
                    modifier = Modifier
                        .fillMaxWidth()
                        .padding(10.dp, 10.dp, 10.dp, 5.dp),
                )

                Spacer(modifier = Modifier.height(10.dp))

                Card(
                    modifier = Modifier
                        .fillMaxWidth()
                        .padding(10.dp),
                    backgroundColor = colorResource(id = R.color.gris_muy_claro)
                ) {
                    Text(
                        text = if (!pagoTarjeta.value) "A DEVOLVER: ${cashInput.value - currentTicket.total}€" else "",
                        fontWeight = FontWeight.Bold,
                        fontSize = 14.sp,
                        modifier = Modifier.padding(10.dp))
                }

                Spacer(modifier = Modifier.height(5.dp))

                Button(
                    enabled = aceptarEnabled.value,
                    onClick = {
                        if(currentTicket._id == currentTable.id_ticket){
                            pago(
                                pagoTarjeta,
                                cashInput,
                                tableViewModel,
                                navController,
                                applicationContext,

                            )
                        }else{
                            tableViewModel.getTicket(currentTable.id_ticket){
                                currentTicket = tableViewModel.currentTicketResponse
                                pago(
                                    pagoTarjeta,
                                    cashInput,
                                    tableViewModel,
                                    navController,
                                    applicationContext
                                )
                            }
                        }
                        currentTicketLines.value = listOf()
                    },
                    modifier = Modifier
                        .fillMaxWidth()
                        .height(80.dp)
                        .padding(10.dp),
                    shape = RoundedCornerShape(5.dp),
                    colors = ButtonDefaults.buttonColors(backgroundColor = colorResource(id = R.color.azul)),
                ) {
                    Text(
                        text = "COBRAR",
                        color = Color.White,
                        fontSize = 12.sp
                    )
                }
                Spacer(modifier = Modifier.height(10.dp))

                Button(
                    onClick = {
                        isCobrarOpen.value = false
                        scope.launch { scaffoldState.drawerState.close() }
                    },
                    modifier = Modifier
                        .fillMaxWidth()
                        .height(80.dp)
                        .padding(10.dp),
                    shape = RoundedCornerShape(5.dp),
                    colors = ButtonDefaults.buttonColors(backgroundColor = colorResource(id = R.color.rojo)),
                ) {
                    Text(
                        text = "CANCELAR",
                        color = Color.White,
                        fontSize = 12.sp
                    )
                }
            }
        }
    }
}

private fun pago(
    pagoTarjeta: MutableState<Boolean>,
    cashInput: MutableState<Float>,
    tableViewModel: TableViewModel,
    navController: NavController,
    applicationContext: Context
) {
    if (!pagoTarjeta.value) {
        if (cashInput.value >= currentTicket.total) {
            currentTicket.cobrado = true
            currentTicket.comensales = currentTable.comensales
            currentTicket.tipo_ticket = "Efectivo"
            currentTicket.id_user_que_cerro = currentUser._id
            currentTicket.date to Date()
            currentTable.id_ticket = "Error"
            currentTable.ocupada = false
            tableViewModel.updateTable(currentTable, currentTable._id)
            tableViewModel.updateTicket(currentTicket, currentTicket._id)
            firstOpenTable = true
            navController.navigate(ScreenNav.MapScreen.route)

        } else {
            Toast.makeText(applicationContext, "Falta dinero en efectivo", Toast.LENGTH_SHORT)
                .show()
        }
    } else {
        currentTicket.tipo_ticket = "Tarjeta"
        currentTicket.cobrado = true
        currentTable.id_ticket = "Error"
        currentTable.ocupada = false
        currentTicket.id_user_que_cerro = currentUser._id
        currentTicket.date to Date()
        tableViewModel.updateTable(currentTable, currentTable._id)
        tableViewModel.updateTicket(currentTicket, currentTicket._id)
        firstOpenTable = true
        navController.navigate(ScreenNav.MapScreen.route)
    }
}
