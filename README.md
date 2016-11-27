Q-and-A
=======

Spletno mesto namenjeno zastavljanju vprašanj in dogovorom
----------------------------------------------------------

Aplikacija bo omogočala:

* zastavljanje anonimnih vprašanj
* točkovanje odgovorov ter prioritiziranje bolje točkovanih odgovorov
* označenje rešenih vprašanj
* komentiranje vprašanj in odgovoror

Poročilo 1. faze - frontend
---------------------------

###  1. Namen aplikacije
Aplikacija omogoča zastavljanje vprašanj, kjer lahko vsi prispevajo k iskanju rešitve. Namenjena je vsem, ki imajo kakršnokoli vprašanje in iščejo pomoč, kot tudi tistim ki želijo svoje znanje deliti in biti v pomoč ostalim. 
Prilagojena je tudi za prenosne naprave, kot so telefoni in tablice.

###  2.  Posebni gradniki
Za navigacijo po strani sem z pomočjo HTML-ja in CSS-ja ustvaril sistem zavihkov.  Narejeni so na podlagi "radio button-ov". Z pomočjo CSS-ja so sekcije, ki ne pripadajo aktivnemu zavihku skrite in obratno. Vse skupaj je oblikovano, tako da je v stilu strani, uporabnik pa nima občutka da gre za enostavno zasnovo.
Ker stran vključuje različne profile, se ti razlikujejo med seboj tudi z pomočjo slike profila. Uporabniku aplikacija omogoča zajem slike z spletno kamero.

### 3. Odziv brskalnikov
Aplikacija je bila preizkušena brskalnikih Chrome, Firefox in Edge. V vseh se aplikacija obnaša kot je bilo predvideno. Na edino "težavo" naletimo, če želimo uporabiti uporabiti funkcijo spletne kamere na Chromu, saj ta dostop do kamere dopušča le z uporabo protokola https.

### 4. Komentar
Ker je bil izdelan bolj ali manj zgolj uporabniški vmesnik, brez pretirane uporabe skriptnih jezikov,  določeni deli še niso povsem delujoči. To vključuje kakršnekoli dele, ki potrebujejo dostop do baze podatkov, kot na primer iskanje vprašanj in odgovorov, iskanje popularnih tem, bazo uporabnikov in podobno. To je cilj v 2. fazi naloge. 

