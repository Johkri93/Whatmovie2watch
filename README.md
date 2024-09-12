# Whatmovie2watch

## Projektbeskrivning

**Whatmovie2watch** är en applikation byggd i .NET som låter användare söka efter filmer baserat på genrer med hjälp av The Movie Database (TMDb) API. Användaren kan mata in en eller flera genrer och få en slumpmässig film baserad på dessa genrer. Applikationen hämtar även tillgängliga genrer från TMDb API för att hjälpa användaren att välja.

### Funktioner:
- Hämta och lista tillgängliga filmgenrer från TMDb API.
- Användaren kan ange genrer genom namn eller ID och få en slumpmässig film som matchar dessa genrer.
- API-nyckel hanteras säkert via en dold `appsetting.json`-fil som inte versioneras till GitHub.
  
## Installation

### Förutsättningar

- .NET SDK installerat ([Ladda ner .NET SDK här](https://dotnet.microsoft.com/download))
- En giltig API-nyckel från TMDb ([Skapa en TMDb API-nyckel här](https://www.themoviedb.org/settings/api))

### Steg för installation

1. **Klona repositoryt:**
   ```bash
   git clone https://github.com/Johkri93/Whatmovie2watch.git
   cd Whatmovie2watch

## Konfiguration av API-nyckel

För att kunna köra applikationen behöver du skapa en `appsetting.json`-fil och lägga in din API-nyckel.

1. Ändra namnet på `appsetting.json.example` till `appsetting.json`:
   ```bash
   cp appsetting.json.example appsetting.json

2. Lägg till din egna api nyckel i appsetting.json inom " "