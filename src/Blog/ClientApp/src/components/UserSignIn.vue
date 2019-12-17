<template>
    <div class="container-fluid">
        <div class="row">
            <label>Username</label>
            <input class="form-control" type="text" v-model="username" />
        </div>
        <div class="row">
            <label>Password</label>
            <input class="form-control" type="password" v-model="password" />
        </div>
        <div class="row">
            <button class="btn-primary" v-on:click="signIn">Sign In</button>
        </div>
    </div>
</template>

<script>
import Axios from "axios";

export default {
    name: 'UserSignIn',
    data: function() {
        return {
            username: null,
            password: null,
        }
    },
    methods: {
        signIn: function(){
            
            let payload = {
                username: this.username,
                password: this.password
            }

            Axios.post("http://localhost:5000/api/sign-in", payload)
                .then(res => {
                    if(res.status >= 200 && res.status < 300){
                        this.$store.commit('signIn');
                        this.$router.push("/admin");
                    }
                    else{
                        alert("Sign in failed!");
                    }
                })
                .catch(err => console.log(err)
            );
        }
    }
}
</script>