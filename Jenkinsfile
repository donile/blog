pipeline {
    agent any

    stages {
        stage('Build') {
            steps {
                sh './build/build.sh'
            }
        }
        stage('Test') {
            steps {
                echo 'Testing..'
            }
        }
        stage('Deploy') {
            when {
                branch 'master'
            }
            steps {
                echo 'Deploying....'
                withCredentials([
                    sshUserPrivateKey(
                        credentialsId: 'deployer', 
                        keyFileVariable: 'DEPLOYER_SSH_PRIVATE_KEY', 
                        usernameVariable: 'DEPLOYER_USERNAME')]) {
                            sh './build/deploy.sh'
                        }
            }
        }
    }
}