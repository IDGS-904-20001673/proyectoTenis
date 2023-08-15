from itertools import groupby
from operator import itemgetter
import json

def filterRecipeBySale(rows):
    grouped_results = []
    for row in rows:
        result_row = {
            'descriptionRecipe': f"{row['nombreProducto']} ({row['nombre']}) - {row['descripccion']}",
            'totalRecipes': row['totalVendido'],
            'totalProfit': row['totalVendido'] * row['precio'],
            'idCatRecipe': row['idProducto']
        }
        grouped_results.append(result_row)
    return json.dumps(grouped_results)
