window.onload = function () {
let listNode, imageNode;

    // Affichage de l'image
  listNode = document.getElementById("categories");
  imageNode = document.getElementById("image");
    let imageLinks = listNode.querySelectorAll("a");

    let i, linkNode, link, image;
    for ( i = 0; i < imageLinks.length; i++ ) {
        linkNode = imageLinks[i];
        linkNode.onclick = function (evt) {
          evt.preventDefault();
          link = this;
            
            imageNode.src = link.getAttribute("href");
          document.getElementById("image").setAttribute("style", "display:block;");
        };

        // Préchargement de l'image
        image = new Image();
        image.src = linkNode.getAttribute("href");
    }
};