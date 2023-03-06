# Test technique pour 7Shapes

Voici mon test technique pour le poste de développeur stagiaire Unity pour l'entreprise 7Shapes. Celui-ci consistait à ajouter des nouvelles fonctionnalités dans un projet pré-existant.

Voici le lien Itch.io: https://doriangms.itch.io/7shapes-test

## Première fonctionnalité: Déplacements des postes

Afin de pouvoir "*drag and drop*" les postes, j'ai implémenté le script *MovableElement* de la même façon que le Worker.
Afin que les zones de sécurités ne se chevauchent pas, j'ai utilisé la fonction *Physics.OverlapBox* avec la position du *Collider* du poste afin de détecter les obstacles et empêcher le mouvement dans ces endroits.
J'ai également ajouté une nouvelle méthode *OnInteractableMouseScroll* dans la classe *InteractableElement* prenant en compte la molette de la souris afin de pouvoir pivoter un poste.

## Seconde fonctionnalité: Interactions entre le Worker et les postes

J'ai implémenté les méthodes *Pick* et *Drop* de la classe *PostController* dans la classe *WorkerController*. Le *DoUpdate* étant un *IEnumerator* j'ai utilisé la fonction *WaitForSeconds* pour attendre une seconde à chaque action.
En bonus, j'ai fait en sorte que le poste n°1 recharge ses stocks automatiquement avec une vitesse définie. J'ai également ajouté des éléments de UI pour les stocks présent dans chaque poste, ainsi qu'une barre de chargement afin de représenter l'ajout de stocks.

## Troisième fonctionnalité: Historique des évenements

J'ai ajouté une *ScrollView* dans le canvas principal ainsi qu'une nouvelle classe *EventHistory* sous forme de singleton afin de pouvoir l'appeler de n'importe où. Dans cette classe, j'ai ajouté une méthode *AddRelocationEvent* afin d'aggrémenter une liste d'évènements pour constituer un historique. Cette méthode va également créer une préfab de bouton. Lors du click de ce bouton, les éléments modifiés vont automatiquement se remettre à leurs états d'origine.
