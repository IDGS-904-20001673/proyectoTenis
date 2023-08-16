from itertools import groupby
from operator import itemgetter
import json

def filterRecipeBySale(rows):
    grouped_results = []
    for row in rows:
        result_row = {
            'descriptionRecipe': f"{row['nombreProducto']}",
            'totalRecipes': row['totalVendido'],
            'idCatRecipe': row['idProducto']
        }
        grouped_results.append(result_row)
    return json.dumps(grouped_results)
