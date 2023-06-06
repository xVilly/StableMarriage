
// Funkcja zwraca tablice par (mężczyźni są indeksami, kobiety wartościami)
int[] solve(List<int>[] menPreferences, List<int>[] womenPreferences) {
    int[] ret = new int[menPreferences.Length];
    int[] lastWomanChecked = new int[menPreferences.Length];
    for (int i = 0; i < ret.Length; i++)
        ret[i] = -1;
    for (int i = 0; i < lastWomanChecked.Length; i++)
        lastWomanChecked[i] = -1;
    for (int i=0; i < menPreferences.Length; i++){
        // Jeżeli mężczyzna ma już parę to jest pomijany
        if (ret[i] != -1)
            continue;
        // Przejście przez wszystkie preferencje mężczyzny
        // * Jeżeli algorytm już raz sprawdzał preferencje mężczyzny
        // a wystąpiła potrzeba powrotu, to pętla zaczyna się od ostatniej sprawdzonej
        // preferencji, aby nie wykonywać identycznych obliczeń kilkukrotnie
        for (int k = lastWomanChecked[i]+1; k < menPreferences[i].Count; k++){
            int manTopPref = menPreferences[i][k] - 1;
            int isTakenBy = -1;
            // Sprawdzenie czy preferowana kobieta jest już zajęta
            for (int j = 0; j < ret.Length; j++){
                if (ret[j] == manTopPref){
                    isTakenBy = j;
                    break;
                }
            }
            if (isTakenBy == -1){
                // Jeżeli jest wolna, to przydzielamy do mężczyzny
                ret[i] = manTopPref;
                Console.WriteLine($"Assigned woman {manTopPref + 1} to man {i + 1}.");
                lastWomanChecked[i] = k;
                break;
            } else {
                // Jeżeli jest zajęta, to porównujemy preferencje kobiety dla
                // poprzedniego i obecnego mężczyzny
                int valueOfCurrentMan = womenPreferences[manTopPref].FindIndex(x => x == isTakenBy+1);
                int valueOfMe = womenPreferences[manTopPref].FindIndex(x => x == i+1);
                if (valueOfCurrentMan < valueOfMe) {
                    // Jeżeli poprzedni mężczyzna jest wyżej w preferencjach kobiety, obecny mężczyzna kontynuuje poszukiwania
                    Console.WriteLine($"Woman {manTopPref + 1} is taken by a higher priority man, can't assign to man {i + 1}. Looking for other options.");
                    lastWomanChecked[i] = k;
                    continue;
                } else {
                    // Jeżeli obecny mężczyzna jest wyżej w preferencjach kobiety, poprzedni mężczyzna traci parę
                    // i kontynuuje poszukiwania, kobieta zostaje przydzielona obecnemu mężczyźnie
                    Console.WriteLine($"Woman {manTopPref + 1} is taken by a lower priority man. Assigning her to man {i + 1}. Making {isTakenBy + 1} look for better options.");
                    ret[i] = manTopPref;
                    ret[isTakenBy] = -1;
                    i = isTakenBy - 1;
                    break;
                }
            }
        }
    }
    return ret;
}

void fill(ref List<int>[] arr)
{
    var r = new Random();
    for (int i=0; i < arr.Length; i++)
    {
        var l = new List<int>();
        while (l.Count < arr.Length)
        {
            var p = r.Next(1, arr.Length+1);
            if (!l.Exists(x=>x==p))
                l.Add(p);
        }
        arr[i] = l;
    }
}

void display(List<int>[] arr)
{
    for (int i=0; i < arr.Length; i++)
    {
        Console.Write($"{(i+1)}: ");
        for (int j = 0; j < arr[i].Count; j++)
            Console.Write($"{arr[i][j]} ");
        Console.WriteLine();
    }
}

int count = 1000;

List<int>[] menPreferences = new List<int>[count];
List<int>[] womenPreferences = new List<int>[count];

fill(ref menPreferences);
fill(ref womenPreferences);

/*menPreferences[0] = new List<int>() { 2, 1, 4, 5, 3 };
menPreferences[1] = new List<int>() { 1,4,3,5,2 };
menPreferences[2] = new List<int>() { 2,3,5,4,1 };
menPreferences[3] = new List<int>() { 3,2,4,1,5 };
menPreferences[4] = new List<int>() { 4,5,3,1,2 };

womenPreferences[0] = new List<int>() { 3,2,1,5,4 };
womenPreferences[1] = new List<int>() { 1,3,2,4,5 };
womenPreferences[2] = new List<int>() { 2,1,3,5,4 };
womenPreferences[3] = new List<int>() { 1,2,3,5,4 };
womenPreferences[4] = new List<int>() { 5,2,4,1,3 };*/


Console.WriteLine("Men preferences:");
//display(menPreferences);
Console.WriteLine("\nWomen preferences:");
//display(womenPreferences);
Console.WriteLine();

var s = solve(menPreferences, womenPreferences);

Console.WriteLine("Final man & woman pairs:");
for(int i=0; i < s.Length; i++)
{
    Console.Write($"{i + 1} & {s[i] + 1}");
    if (i != s.Length - 1)
        Console.Write(", ");
}
Console.WriteLine();