package inter.intermodular.screens.table_payment

import androidx.compose.foundation.ExperimentalFoundationApi
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.GridCells
import androidx.compose.foundation.lazy.LazyVerticalGrid
import androidx.compose.foundation.shape.RoundedCornerShape
import androidx.compose.material.*
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.HighlightOff
import androidx.compose.runtime.Composable
import androidx.compose.runtime.MutableState
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.res.colorResource
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import androidx.compose.ui.window.Dialog
import com.orhanobut.logger.Logger
import inter.intermodular.R
import inter.intermodular.models.ProductModel
import inter.intermodular.support.*
import inter.intermodular.view_models.TableViewModel
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.delay
import kotlinx.coroutines.launch
import kotlin.math.round

@OptIn(ExperimentalFoundationApi::class)
@Composable
fun ShowAlertDialogFamilyProducts(
    isDialogOpen: MutableState<Boolean>,
    tableViewModel: TableViewModel,
    familyProductList: MutableState<List<ProductModel>>,
    scope: CoroutineScope,
    afterFirstProduct: MutableState<Boolean>,
    currentTicketLines: MutableState<List<ProductModel>>,
    totalBill: MutableState<Float>,
    productClicked: MutableState<Boolean>,
) {
    currentProductList = tableViewModel.familyProductsResponse

    if(isDialogOpen.value) {
        if (!familyProductList.value.isNullOrEmpty()){
            Dialog(onDismissRequest = { isDialogOpen.value = false }) {
                Surface(
                    modifier = Modifier
                        //.fillMaxSize()
                        .padding(10.dp, 20.dp),
                    shape = RoundedCornerShape(5.dp),
                    color = Color.White
                ) {
                    Column(
                        horizontalAlignment = Alignment.CenterHorizontally,
                        verticalArrangement = Arrangement.Center
                    ) {
                        Row(
                            horizontalArrangement = Arrangement.Center,
                            verticalAlignment = Alignment.CenterVertically
                        ){
                            Text(
                                text = currentFamily.name,
                                fontSize = 20.sp,
                                fontWeight = FontWeight.Bold,
                                color = colorResource(id = R.color.azul_oscuro),
                                modifier = Modifier.padding( 40.dp, 10.dp, 20.dp, 10.dp)
                            )
                            IconButton(
                                onClick = { isDialogOpen.value = false },
                                modifier = Modifier.padding( 20.dp, 8.dp, 5.dp, 10.dp)
                            ) {
                                Icon(
                                    imageVector = Icons.Filled.HighlightOff,
                                    contentDescription = "",
                                    tint = colorResource(id = R.color.rojo)
                                )
                            }
                        }
                        LazyVerticalGrid(
                            cells = GridCells.Adaptive(100.dp),
                            // modifier = Modifier.fillMaxSize()
                        ){
                            //for ( i in 0 until familyProductList.value.count()){
                            for (product in familyProductList.value){
                                item {
                                    Button(
                                        modifier = Modifier
                                            .height(80.dp)
                                            .width(100.dp)
                                            .padding(5.dp),

                                        colors = ButtonDefaults.buttonColors(colorResource(id = R.color.azul_oscuro)),
                                        onClick = {

                                            //TODO, logica de crear ticket y anyadir linea_ticket
                                            //scope.launch {
                                                currentProduct = product
                                                Logger.i("Producto seleccionado $currentProduct")
                                                if(currentTicketLines.value.isEmpty()){

                                                    tableViewModel.createTicket()
                                                    //delay(100)
                                                    currentProduct.id_ticket = currentTicket._id
                                                    tableViewModel.createTicketLine(currentProduct)
                                                    //recalculate(currentTicketLines = currentTicketLines, totalBill = totalBill)
                                                   // delay(100)

                                                }
                                                else{
                                                    var exists = false
                                                    for (line in currentTicketLines.value){
                                                        if (line.name == currentProduct.name){
                                                            exists = true
                                                            line.cantidad++
                                                            line.total = line.cantidad * line.precio
                                                            //if (line.total.toString().length >= 5)
                                                               // line.total = line.total.toString().substring(0,4).toFloat()
                                                            tableViewModel.updateTicketLine(line, line._id)
                                                            val toGenerateLines = currentTicketLines.value
                                                            currentTicketLines.value = listOf()
                                                            currentTicketLines.value = toGenerateLines
                                                          //  delay(400)
                                                        }
                                                    }

                                                    if (exists) Logger.d("Producto sumado")
                                                    else{
                                                        tableViewModel.createTicketLine(currentProduct)
                                                        currentTicketLines.value = currentTicketLines.value + tableViewModel.currentTicketLineResponse
                                                    }
                                                   // delay(100)
                                                }
                                                currentTable.id_ticket = currentTicket._id
                                                //tableViewModel.updateTable(currentTable, currentTable._id)
                                               // delay(100)
                                                productClicked.value = true
                                               // isDialogOpen.value = false
                                           // }

                                        }) {
                                        Text(
                                            text =  if (product.name.length > 5)
                                                product.name.substring(0, 5)
                                            else product.name,
                                            fontSize = 14.sp,
                                            color = Color.White,
                                            fontWeight = FontWeight.Bold
                                        )
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}