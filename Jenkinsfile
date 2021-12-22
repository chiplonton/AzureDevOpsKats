node('docker') {

    stage('Git checkout') {
        git branch: 'master', credentialsId: 'gihub-key', url: 'git@github.com:stuartshay/AzureDevOpsKats.git'
    }

    stage('Docker deploy image') {
         sh '''mv docker/azuredevopskats-web-build.dockerfile/.dockerignore .dockerignore
         docker build -f docker/azuredevopskats-web-build.dockerfile/Dockerfile --build-arg BUILD_NUMBER=${BUILD_NUMBER} --build-arg VCS_REF=`git rev-parse --short HEAD` -t stuartshay/azuredevopskats:2.2.10-build .'''

         withCredentials([usernamePassword(credentialsId: 'docker-hub-navigatordatastore', usernameVariable: 'DOCKER_HUB_LOGIN', passwordVariable: 'DOCKER_HUB_PASSWORD')]) {
            sh "docker login -u ${DOCKER_HUB_LOGIN} -p ${DOCKER_HUB_PASSWORD}"
        }
        sh '''docker push stuartshay/azuredevopskats:2.2.10-build'''
    }

}
