<template>
    <div>
        <h2 class="text-center">{{title}}</h2>
        <div class="text-center">by: {{author}}</div>
        <div class="container-fluid" style="max-width: 500px">{{body}}</div>
    </div>
</template>

<script>
export default {
    name: 'BlogPost',
    data: function(){
        return {
            title: "Example Blog Post",
            author: "Mark Donile",
            body: "This is an example of a blog post.  It is not very long, but it is long enough.  How long does it go."
        }
    }
}
</script>