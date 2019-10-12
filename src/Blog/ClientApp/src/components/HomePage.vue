<template>
    <div class="container-fluid">
        <div class="row">
            <div class="col-4 offset-4">
                <blog-post/>
            </div>
        </div>
    </div>
</template>

<script>
import BlogPost from './BlogPost';

export default {
    name: 'HomePage',
    components: {
        'blog-post': BlogPost
    }
}
</script>