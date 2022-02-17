package inter.intermodular.screens.table_payment

import android.content.Context
import android.widget.Toast
import androidx.compose.foundation.ExperimentalFoundationApi
import androidx.compose.foundation.combinedClickable
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.GridCells
import androidx.compose.foundation.lazy.GridItemSpan
import androidx.compose.foundation.lazy.LazyVerticalGrid
import androidx.compose.material.*
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Menu
import androidx.compose.material.icons.filled.Textsms
import androidx.compose.runtime.Composable
import androidx.compose.runtime.MutableState
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.res.colorResource
import androidx.compose.ui.unit.dp
import com.orhanobut.logger.Logger
import inter.intermodular.R
import inter.intermodular.models.ProductModel
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.launch

@OptIn(ExperimentalFoundationApi::class)
@Composable
fun TicketContentComponent(
    currentTicketLines: MutableState<List<ProductModel>>,
    applicationContext: Context,
    currentLine: MutableState<ProductModel>,
    isLineOptionsOpen: MutableState<Boolean>,
    snackbarHostState: SnackbarHostState,
    scope: CoroutineScope
) {
    Card(
        backgroundColor = colorResource(id = R.color.gris_muy_claro),
        modifier = Modifier
            .fillMaxWidth()
            .fillMaxHeight(1f)
            .padding(5.dp)
    ) {
        LazyVerticalGrid(
            cells = GridCells.Fixed(5),
            horizontalArrangement = Arrangement.Center,
            verticalArrangement = Arrangement.Top,
            contentPadding = PaddingValues(5.dp),
        ) {
           // for (i in 0 until currentTicketLines.value.count()) {
            for(product in currentTicketLines.value){
                if(product.name != "Error"){
                    product.total = product.cantidad * product.precio

                    item {
                        Card(

                            modifier = Modifier.combinedClickable(
                                onLongClick = {
                                    currentLine.value = product
                                    isLineOptionsOpen.value = true
                                },
                                onClick = {
                                    Toast.makeText(applicationContext, "Manten pulsado para ver opciones", Toast.LENGTH_SHORT).show()
                                    currentLine.value = product
                                }
                            )
                        ){
                            Text(
                                text = "${product.cantidad}",
                                modifier = Modifier
                                    .fillMaxSize()
                                    .padding(10.dp)
                            )
                        }
                    }
                    item(span = { GridItemSpan(2) }) {
                        Card(
                            modifier = Modifier.combinedClickable(
                                onLongClick = {
                                    currentLine.value = product
                                    isLineOptionsOpen.value = true
                                },
                                onClick = {
                                    Toast.makeText(applicationContext, "Manten pulsado para ver opciones", Toast.LENGTH_SHORT).show()
                                    currentLine.value = product
                                }
                            )
                        ){
                            Row (
                                modifier = Modifier.fillMaxWidth()
                            ) {
                                Text(
                                    text = product.name,
                                    modifier = Modifier
                                        .fillMaxWidth(0.8f)
                                        .padding(10.dp)
                                )
                                if(product.comentario.isNotBlank()
                                    || product.comentario.isNotEmpty()
                                ){
                                    IconButton(
                                        modifier = Modifier.size(20.dp),
                                        onClick = {
                                            currentLine.value = product
                                            Logger.d("Click en options icon")
                                            scope.launch{
                                                snackbarHostState.currentSnackbarData?.dismiss()
                                                snackbarHostState.showSnackbar("")
                                            }
                                        }) {
                                        Icon(Icons.Filled.Textsms, contentDescription = "Ver comentario")
                                    }
                                }
                            }
                        }
                    }
                    item {
                        Card(
                            modifier = Modifier.combinedClickable(
                                onLongClick = {
                                    currentLine.value = product
                                    isLineOptionsOpen.value = true
                                },
                                onClick = {
                                    Toast.makeText(applicationContext, "Manten pulsado para ver opciones", Toast.LENGTH_SHORT).show()
                                    currentLine.value = product
                                }
                            )
                        ){
                            Text(
                                text = "${product.precio}€",
                                modifier = Modifier
                                    .fillMaxSize()
                                    .padding(10.dp)
                            )
                        }
                    }
                    item {
                        Card(
                            modifier = Modifier.combinedClickable(
                                onLongClick = {
                                    currentLine.value = product
                                    isLineOptionsOpen.value = true
                                },
                                onClick = {
                                    Toast.makeText(applicationContext, "Manten pulsado para ver opciones", Toast.LENGTH_SHORT).show()
                                    currentLine.value = product
                                }
                            )
                        ){
                            Text(
                                text =
                                if (product.total.toString().length >= 5)
                                    "${product.total.toString().substring(0,4).toFloat()}€"
                                else "${product.total}€",
                                modifier = Modifier
                                    .fillMaxSize()
                                    .padding(10.dp)
                            )
                        }
                    }
                }
            }
        }
    }
}