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
            steps {
                echo 'Deploying....'
            }
        }
    }
}