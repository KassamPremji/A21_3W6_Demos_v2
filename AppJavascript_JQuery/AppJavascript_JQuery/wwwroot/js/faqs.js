let $ = function (id) {
    return document.getElementById(id);
};

function actived (sujet) {
    sujet.classList.toggle("active");
}

function colaspeExpand (sujet) {
  sujet.classList.toggle("plus");
  sujet.classList.toggle("minus");

    sujet.nextElementSibling.classList.toggle("closed")
        .nextElementSibling.classList.toggle("open");
}

window.onload = function () {
    let h2Elements = $("faqs").querySelectorAll("h2");

    for (let i = 0; i < h2Elements.length; i++ ) {
        let h2Node = h2Elements[i];
        // Attacher le gestionnaire d'évènement
        h2Node.onmouseover = function(){
            actived(this);
        };
        h2Node.onmouseout = function(){
            actived(this);
        };

        h2Node.onclick = function(){
            colaspeExpand(this);
        };
    }
};
