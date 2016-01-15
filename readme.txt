Interface pour jeux d'échecs----------------------------
Url     : http://codes-sources.commentcamarche.net/source/100999-interface-pour-jeux-d-echecsAuteur  : FredJustDate    : 23/04/2015
Licence :
=========

Ce document intitulé « Interface pour jeux d'échecs » issu de CommentCaMarche
(codes-sources.commentcamarche.net) est mis à disposition sous les termes de
la licence Creative Commons. Vous pouvez copier, modifier des copies de cette
source, dans les conditions fixées par la licence, tant que cette note
apparaît clairement.

Description :
=============

Bonjour
<br />
<br />Pour un projet Arduino j'avais besoin d'un module qui con
trole les coups valides aux échecs.
<br />j'en ai rapidement créer un et pour q
u'il soit plus présentable j'ai cherché une méthode
<br />graphique acceptable.
 
<br />pour un premier programme VB 2010 après avoir quitter VB 6 il y a 13 an
s ,
<br />(ma derniere source date de 2002 ;-)
<br />je suis content du résult
at, je vous le partage.
<br />
<br />Fonctinnalités :
<br />vérification des 
coups valides
<br />import/export en PGN en notation algébrique
<br />gestion 
du roque et de la prise en passant
<br />export et import en notation FEN
<br 
/>affichage graphique 
<br />pieces avec transparences et mouvements possibles 
avec symbols
<br />( vert : coup valide, bleu : coup possible mais non valide ,
 rouge : cette piece peut la capturer)
<br />
<br />les pieces sont des PNG av
ec transparence (on peut utiliser paint.net pour les retoucher)
<br />l'échiqui
er se redimensionne à la taille de la fentre automatiquement
<br />(les pièces 
sont en 88x88 d'origine je n'ai pas testé en Full HD, 
<br />il est possible de
 mettre des images plus larges si nécessaire)
<br />elles proviennent de ces fi
chier ZIP :
<br /><a href='http://ixian.com/chess/jin-piece-sets/' rel='nofollo
w' target='_blank'>http://ixian.com/chess/jin-piece-sets/</a>
<br />
<br /><b>
EDIT : les bord ne sont pas sufisament long en Full HD 
<br />soit je modifie l
a ressource soit on trace le bord une 2° fois en décallé </b>
<br />
<br />il 
n'y a pas d'animation (à voir si on peut les ajouter)
<br />
<br />évolutions 
possibles
<br />export en fichier txt de la partie au format PGN
<br />(il n'y
 a plus qu'a rajouter l'entete PGN)
<br />et importation des fichiers PGN (à ne
 pas confondre avec les PNG) pour lecture
<br />
<br />version compilée sur ma
 dropbox:
<br /><a href='https://www.dropbox.com/s/oc199ocnfsv4875/TestGraphic.
exe?dl=0' rel='nofollow' target='_blank'>https://www.dropbox.com/s/oc199ocnfsv48
75/TestGraphic.exe?dl=0</a>
<br />
<br />mon projet ARDUINO
<br /><a href='ht
tp://www.cpe95.org/spip.php?rubrique128' rel='nofollow' target='_blank'>http://w
ww.cpe95.org/spip.php?rubrique128</a>
<br />je cherche des idées de programmati
on pour la reconnaissance des coups en automatique ...
<br />
<br />Merci
