from itertools import groupby
from operator import itemgetter
import json


def filterRawMaterialsByRecipe(rows):
    grouped_results = []
    for key, group in groupby(rows, key=itemgetter('nombreMateriaPrima')):
        group_list = list(group)
        total_quantity_raw_material_used = sum(row['cantidadUsoMateria'] for row in group_list)
        first_row = group_list[0]
        result_row = {
            'totalQuantityRawMaterialUsed': total_quantity_raw_material_used,
            'name': first_row['nombreMateriaPrima'],
            'quantityWarehouse': first_row['cantidadTotal'],
            'description': first_row['nombreProducto']
        }
        grouped_results.append(result_row)
    return json.dumps(grouped_results)
