# backend

W naszej aplikacji wyróżniamy kilka głównych funkcjonalności:
1. Aktywności - w ramach korzystania z programu użytkownik może dodać aktywność, dla której określa jej kategorie, a za jej wykonanie otrzymuje wyznaczoną ilość punktów, w późniejszym czasie aplikacja oblicza sumę jego punktów i dopisuje go do ściany wyników.
2. Rachunki - użytkownik może nie tylko śledzić swoje wydatki, które zalicza do wydatków własnych, ale może on również śledzić wydatki w ramacgh grupy.
3. Grupy - w aplikacji mamy do czynienia z grupami, w ich obrębie możemy wykonywać dosłownie te same akcje, które możemy zrobić indywidualnie, ale również prowadzić np. wspólne wydatki.
4. ToDoListy - ta część aplikacji jest odpowiedzialna za tworzenie todolist, w naszym programie użytkownik ma możliwość skorzystania z przygotowanych wcześniej templatów i na ich podstawie może on tworzyć kolejne todolist, może on oczywiście tworzyć również swoje. W ramach todolisty, użytkownik nadaje jej nazwę, a w następnej kolejności dodaje sekcje, do których poźniej może dodać task/zadania oraz przypisać im odpowiedni priorytet.

KONFIGURACJA

po stronie backendu, nie jest ona zbytnio skomplikowana, wystarczy odpalić:
1. Package Manager Console => wpisać komende Database-Update, dzieki ktorej program utworzy nam potrzebną baze danych (tutaj oczywiście są potrzebne appsettings)
2. Następnie odpalić backend, a z poziomu swaggera uruchomić 3 endpointy odpowiednio
  - do utworzenia ról dla aplikacji
  - do utworzenia templatow todolist
  - do utworzenia kategorii aplikacji
