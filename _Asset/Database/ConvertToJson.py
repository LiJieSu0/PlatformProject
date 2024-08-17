import pandas as pd
import json

# Load the Excel file
excel_file_path = './Lines.xlsx'
df = pd.read_excel(excel_file_path)

# Initialize an empty dictionary
json_data = {}

# Iterate over the rows of the DataFrame
for index, row in df.iterrows():
    header = row['Key']  # Assuming the column for the header is named 'Header'
    lines = row['Lines']
    option1 =row['Option1']
    option2=row['Option2']
    option3=row['Option3']
    option4=row['Option4']
    result1 =row['Result1']
    result2=row['Result2']
    result3=row['Result3']
    result4=row['Result4']
    if(header not in json_data):
        json_data[header]={
            "Lines":[],
            "Options":[],
            "Results":[]
        }

    json_data[header]["Lines"].append(lines)
    if(not pd.isna(option1)):
        json_data[header]["Options"].append(option1)
    if(not pd.isna(option2)):
        json_data[header]["Options"].append(option2)
    if(not pd.isna(option3)):
        json_data[header]["Options"].append(option3)
    if(not pd.isna(option4)):
        json_data[header]["Options"].append(option4)

    if(not pd.isna(result1)):
        json_data[header]["Results"].append(result1)
    if(not pd.isna(result2)):
        json_data[header]["Results"].append(result2)
    if(not pd.isna(result3)):
        json_data[header]["Results"].append(result3)
    if(not pd.isna(result4)):
        json_data[header]["Results"].append(result4)



# Convert the dictionary to a JSON string
json_string = json.dumps(json_data, indent=4)

# Save the JSON string to a file
json_file_path = './output_file.json'
with open(json_file_path, 'w') as json_file:
    json_file.write(json_string)

print("Excel file has been converted to JSON and saved as", json_file_path)