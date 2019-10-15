<template>
    <h1>AdminHome.vue</h1>
</template>

<script>
export default {
    name: "AdminHome",
    async beforeRouteEnter(to, from, next){
        let authenticated = false;
        
        if(!authenticated){

            next( { name: 'UserSignIn' } );

        }
        
        next( );
    }
}
</script>