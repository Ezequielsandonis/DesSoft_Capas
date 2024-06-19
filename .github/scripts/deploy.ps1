param (
    [string]$ftpServer,
    [string]$ftpUsername,
    [string]$ftpPassword,
    [string]$localPath,
    [string]$remotePath
)

function UploadDirectory {
    param (
        [string]$ftpServer,
        [string]$ftpUsername,
        [string]$ftpPassword,
        [string]$localPath,
        [string]$remotePath
    )

    $localPath = $localPath -replace "\\", "/"
    $remotePath = $remotePath -replace "\\", "/"
    
    foreach ($item in Get-ChildItem -Path $localPath -Recurse) {
        $relativePath = $item.FullName.Substring($localPath.Length).TrimStart("\\")

        if ($item.PSIsContainer) {
            $ftpUri = "ftp://$ftpServer/$remotePath/$relativePath/"
            $ftpWebRequest = [System.Net.FtpWebRequest]::Create($ftpUri)
            $ftpWebRequest.Credentials = New-Object System.Net.NetworkCredential($ftpUsername, $ftpPassword)
            $ftpWebRequest.Method = [System.Net.WebRequestMethods+Ftp]::MakeDirectory

            try {
                $ftpWebResponse = $ftpWebRequest.GetResponse()
                $ftpWebResponse.Close()
            } catch {
                Write-Host "El directorio ya existe: $ftpUri"
            }
        } else {
            $ftpUri = "ftp://$ftpServer/$remotePath/$relativePath"
            $ftpWebRequest = [System.Net.FtpWebRequest]::Create($ftpUri)
            $ftpWebRequest.Credentials = New-Object System.Net.NetworkCredential($ftpUsername, $ftpPassword)
            $ftpWebRequest.Method = [System.Net.WebRequestMethods+Ftp]::UploadFile

            $fileContent = [System.IO.File]::ReadAllBytes($item.FullName)
            $ftpWebRequest.ContentLength = $fileContent.Length
            $requestStream = $ftpWebRequest.GetRequestStream()
            $requestStream.Write($fileContent, 0, $fileContent.Length)
            $requestStream.Close()

            $ftpWebResponse = $ftpWebRequest.GetResponse()
            $ftpWebResponse.Close()
        }
    }
}

UploadDirectory -ftpServer $ftpServer -ftpUsername $ftpUsername -ftpPassword $ftpPassword -localPath $localPath -remotePath $remotePath

Write-Host "Despliegue completo"
