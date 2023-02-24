## Timery

Timery wielowątkowe korzystają z puli wątków. Wiąże się to z tym, że callbacki, dalej zwane metodami, zdefiniowane do wykonania przez timer 
mogą odpalać się w różnych wątkach.
Po upływie ustalonego czasu jest odpalana metoda pomimo tego, że poprzednie wykonanie mogło się jeszcze nie zakończyć, dlatego zaleca się
nie stosować blokad wewnątrz tych metod - unikać loków.
Oznacza to również, że metody te muszą być bezpieczne wątkowo.

Precyzja zegara systemowego mieści się w przedziale od 10 do 20ms, a to na nim bazując timery 
w podstawowej konfiguracji (można to zmienić, temat do zamodzielnego zgłębienia dla zainteresowanych).  
To oznacza, że mogą pojawić się opóźnienia między kolejnymi wywołaniami metod. 
Dlatego jeśli wymagana jest większa precyzja to trzeba znaleźć dokładniejszy sposób mierzenia czasu.

Po zakończeniu używania timera trzeba go zwolnić poprzez wywołanie metody `Dispose` więc taka klasa, który używa timerów
powinna implementować interfejs `IDisposable`.  
Alternatywnie można użyć konstrukcji `using`.

### System.Threading.Timer (wielowątkowy)
Jeśli zegar ma zostać użyty jednorazowo to w ostatnim parametrze ustawiany wartość `Timeout.Infinite`.
Odstęp czasowy można zmienić za pomocą metody `Change`.
Nawet po wywołaniu `Dispose` callback może się wykonać bo timer zakolejkował sobie już wcześniej jakieś zadania do wykonania.
Można użyć przeciążenia `Dispose(WaitHandle)` aby zaczekać na zakończenie wykonania zakolejkowanych zadań.

### System.Timers.Timer (wielowątkowy)
Jest wrapperem dla klasy `System.Threading.Timer`. Ma kilka dodatkowych możliwości:
* Odpala metodę po zdarzeniu Elapsed co zdefiniowaną liczbę milisekund we własności `Interval`.
* Można włączać i wyłączać zegar poprzez ustawienie właściwości `Enabled`. Można stosować zamiennie z metodami `Start` i `Stop`.
* Metoda może wykonać się raz lub wiele razy (autoReset: true).

Jeśli odpalana metoda jest synchroniczna to wyjątki w niej rzucane są tłumione (ma się zmienić w przyszłości).
W przypadku asynchronicznych metod (używających `await`) wyjątki są propagowane do wątku wywołującego.

### System.Threading.PeriodicTimer (wielowątkowy)

Może być tylko jeden konsumer metody WaitForNextTickAsync.  
Zachowanie timera jest podobne do ustawiania właściwości auto-reset we wcześniejszym timerze.  
Ale wywołania nie mogą odbywać się równocześnie, więc kolejne wywołanie metody nie zacznie się dopóki nie skończy się poprzednie.  
Powinno się jednak zadbać o to żeby tick był dłuższy niż czas wykonania metody.  
Wywołanie metody `Dispose` usunie nie rozpoczęte jeszcze ale zakolejkowane wywołania.  
Anulowanie przekazanego do `WaitForNextTickAsync` cancellationTokena anuluje tylko kolejne wywołanie.

### Pozostałe
Reszta zegarów jest używane przez aplikacje z UI (Windows Forms, WPF). Są to zegary jednowątkowe.
* System.Windows.Forms.Timer
* System.Web.UI.Timer

Źródła:  
[1] https://www.robofest.net/2019/TimerReport.pdf  
https://learn.microsoft.com



