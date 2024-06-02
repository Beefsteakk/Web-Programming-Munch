allPost = document.querySelectorAll('.post');
allPost.forEach(function(e) {
    e.addEventListener('click', function(obj) {
        if (obj.target.localName != "button")
            window.location.href = "/Posts/" + e.id;
    })
});

likeButton = document.querySelectorAll('.post-footer > button');
allPost.forEach(function(e) {
    e.addEventListener('click', function(obj) {
        console.log("Yes")
    })
});