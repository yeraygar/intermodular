package inter.intermodular.screens.table_payment

import androidx.compose.foundation.ExperimentalFoundationApi
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.GridCells
import androidx.compose.foundation.lazy.GridItemSpan
import androidx.compose.foundation.lazy.LazyVerticalGrid
import androidx.compose.material.Card
import androidx.compose.material.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.MutableState
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.res.colorResource
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.text.style.TextDecoration
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import inter.intermodular.R

@OptIn(ExperimentalFoundationApi::class)
@Composable
fun TicketHeadComponent(totalBill: MutableState<Float>) {
    Card(
        backgroundColor = colorResource(id = R.color.gris_muy_claro),
        modifier = Modifier
            .fillMaxWidth()
            .padding(5.dp)
    ) {
        LazyVerticalGrid(
            cells = GridCells.Fixed(5),
            contentPadding = PaddingValues(5.dp),
            //modifier = Modifier.padding(5.dp)
        ) {
            item(span = { GridItemSpan(5) }) {
                Column(
                    modifier = Modifier.fillMaxWidth(),
                    horizontalAlignment = Alignment.End,
                    verticalArrangement = Arrangement.Center
                ) {
                    Card(
                        backgroundColor = colorResource(id = R.color.gris_muy_claro),
                        modifier = Modifier
                    ) {
                        Text(
                            text = "TOTAL CUENTA  :   ${totalBill.value}€ ",
                            modifier = Modifier
                                .padding(30.dp, 10.dp),
                            fontWeight = FontWeight.ExtraBold,
                            fontSize = 16.sp,
                            color = colorResource(id = R.color.azul_oscuro)
                        )
                    }
                }
            }
            item {

                Column(
                    modifier = Modifier.fillMaxWidth(),
                    horizontalAlignment = Alignment.End,
                    verticalArrangement = Arrangement.Center
                ) {
                    Card(
                        backgroundColor = colorResource(id = R.color.gris_muy_claro),
                        modifier = Modifier
                            .fillMaxWidth()
                        //.padding(10.dp)
                    ) {
                        Text(
                            text = "CANT.",
                            modifier = Modifier
                                .fillMaxSize()
                                .padding(5.dp),
                            fontWeight = FontWeight.ExtraBold,
                            fontSize = 12.sp,
                            textDecoration = TextDecoration.Underline,
                            color = colorResource(id = R.color.azul_oscuro)
                        )
                    }
                }
            }
            item(span = { GridItemSpan(2) }) {
                Column(
                    modifier = Modifier.fillMaxWidth(),
                    horizontalAlignment = Alignment.End,
                    verticalArrangement = Arrangement.Center
                ) {
                    Card(
                        backgroundColor = colorResource(id = R.color.gris_muy_claro),
                        modifier = Modifier
                            .fillMaxWidth()
                        // .padding(10.dp)
                    ) {
                        Text(
                            text = "NOMBRE",
                            modifier = Modifier
                                .fillMaxSize()
                                .padding(5.dp),
                            fontWeight = FontWeight.ExtraBold,
                            fontSize = 12.sp,
                            textDecoration = TextDecoration.Underline,
                            color = colorResource(id = R.color.azul_oscuro)
                        )
                    }
                }

            }
            item {
                Column(
                    modifier = Modifier.fillMaxWidth(),
                    horizontalAlignment = Alignment.End,
                    verticalArrangement = Arrangement.Center
                ) {
                    Card(
                        backgroundColor = colorResource(id = R.color.gris_muy_claro),
                        modifier = Modifier
                            .fillMaxWidth()
                        //.padding(10.dp)
                    ) {
                        Text(
                            text = "$/Un.",
                            modifier = Modifier
                                .fillMaxSize()
                                .padding(5.dp),
                            fontSize = 12.sp,
                            fontWeight = FontWeight.ExtraBold,
                            textDecoration = TextDecoration.Underline,
                            color = colorResource(id = R.color.azul_oscuro)
                        )
                    }
                }
            }
            item {
                Column(
                    modifier = Modifier.fillMaxWidth(),
                    horizontalAlignment = Alignment.End,
                    verticalArrangement = Arrangement.Center
                ) {
                    Card(
                        backgroundColor = colorResource(id = R.color.gris_muy_claro),
                        modifier = Modifier
                            .fillMaxWidth()
                        //.padding(10.dp)
                    ) {
                        Text(
                            text = "TOT.",
                            modifier = Modifier
                                .fillMaxSize()
                                .padding(5.dp),
                            fontWeight = FontWeight.ExtraBold,
                            fontSize = 12.sp,
                            textDecoration = TextDecoration.Underline,
                            color = colorResource(id = R.color.azul_oscuro)
                        )
                    }
                }
            }
        }
    }
}
