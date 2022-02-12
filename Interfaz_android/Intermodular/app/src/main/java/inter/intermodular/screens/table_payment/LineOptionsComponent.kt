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
import androidx.compose.material.icons.filled.ChatBubble
import androidx.compose.material.icons.filled.Textsms
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
import inter.intermodular.view_models.TableViewModel
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.launch
import java.lang.Exception
import java.util.*

@OptIn(ExperimentalFoundationApi::class)
@Composable
fun ShowAlertDialogLineOptions(
    isLineOptionsOpen: MutableState<Boolean>,
    tableViewModel: TableViewModel,
    applicationContext: Context,
    scope: CoroutineScope,
    currentLine: MutableState<ProductModel>
) {
    val aceptarEnabled = remember { mutableStateOf(false) }
    val cashOk = remember { mutableStateOf(false) }
    val pagoTarjeta = remember { mutableStateOf(false) }
    val commentInput = remember { mutableStateOf("${currentLine.value.comentario}") }

    //cashOk.value = (cashInput.value >= currentTicket.total)
    if (pagoTarjeta.value) aceptarEnabled.value = true
    else aceptarEnabled.value = cashOk.value

    Dialog(onDismissRequest = { isLineOptionsOpen.value = false }) {
        Surface(
            modifier = Modifier
                //.width(400.dp)
                //.height(600.dp)
                .padding(10.dp)
                .fillMaxWidth(),
            shape = RoundedCornerShape(5.dp),
            color = Color.White
        ) {
            Column(
                horizontalAlignment = Alignment.CenterHorizontally,
                verticalArrangement = Arrangement.Center,
                modifier = Modifier
                     .fillMaxWidth()
                    .padding(10.dp)
            ) {

                LazyVerticalGrid(
                    cells = GridCells.Fixed(1),
                    modifier = Modifier.fillMaxWidth(),
                    contentPadding = PaddingValues(10.dp)
                ) {

                    item {
                        Column(
                            horizontalAlignment = Alignment.CenterHorizontally,
                            verticalArrangement = Arrangement.Center,
                            modifier = Modifier
                                // .fillMaxSize()
                                .padding(10.dp)
                        ) {

                            Card(
                                backgroundColor = colorResource(id = R.color.gris_muy_claro),
                                modifier = Modifier
                                    .fillMaxWidth()
                                    .padding(10.dp)
                            ) {
                                Text(
                                    text = "LINEA ELEGIDA ${currentLine.value.name} :  ${currentLine.value.cantidad}",
                                    fontWeight = FontWeight.Bold,
                                    fontSize = 16.sp,
                                    modifier = Modifier.padding(10.dp)
                                )

                            }
                        }
                    }


                    item {

                        Button(
                            onClick = {
                                //TODO DELETE LINEA TICKET NUBE Y LISTA
                                isLineOptionsOpen.value = false
                            },
                            modifier = Modifier
                                .fillMaxWidth()
                                .height(100.dp)
                                .padding(10.dp, 15.dp),
                            shape = RoundedCornerShape(5.dp),
                            colors = ButtonDefaults.buttonColors(backgroundColor = colorResource(id = R.color.oscuro)),
                        ) {
                            Text(
                                text = "ELIMINAR LINEA",
                                color = Color.White,
                                fontSize = 12.sp
                            )
                        }
                    }

                    item {
                        OutlinedTextField(
                            //  enabled = !pagoTarjeta.value,
                            value = commentInput.value,
                            onValueChange = { it.also { commentInput.value = it } },
                            label = {
                                Text(
                                    text = "Comentario",
                                    style = TextStyle(
                                        color = colorResource(id = R.color.gris_claro)
                                    ),
                                )
                            },
                            placeholder = {
                                Text(
                                    text = "Comentario",
                                    style = TextStyle(
                                        color = colorResource(id = R.color.gris_claro)
                                    ),
                                )
                            },
                            singleLine = false,
                            keyboardOptions = KeyboardOptions(keyboardType = KeyboardType.Text),
                            trailingIcon = {
                                val image = Icons.Filled.Textsms
                                IconButton(onClick = { }
                                ) { Icon(imageVector = image, "Comentario") }
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
                    }
                    item {
                        Button(
                            enabled = aceptarEnabled.value,
                            onClick = {
                                //TODO UPDATE LINEA TICKET EN LISTA Y BBDD
                                      isLineOptionsOpen.value = false
                            },
                            modifier = Modifier
                                .fillMaxWidth()
                                .height(80.dp)
                                .padding(10.dp),
                            shape = RoundedCornerShape(5.dp),
                            colors = ButtonDefaults.buttonColors(backgroundColor = colorResource(id = R.color.azul)),
                        ) {
                            Text(
                                text = "GUARDAR COMENTARIO",
                                color = Color.White,
                                fontSize = 12.sp
                            )
                        }
                    }
                    item {

                        Button(
                            onClick = { isLineOptionsOpen.value = false },
                            modifier = Modifier
                                .fillMaxWidth()
                                .height(100.dp)
                                .padding(10.dp,20.dp),
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
    }
}
