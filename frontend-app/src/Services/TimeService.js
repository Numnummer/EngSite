export default function GetDateTimeNow(){
    const currentDateTime = new Date();

    const year = currentDateTime.getFullYear();
    const month = currentDateTime.getMonth() + 1; // Adding 1 since months are zero-based
    const date = currentDateTime.getDate();

    const hours = currentDateTime.getHours();
    const minutes = currentDateTime.getMinutes();
    const seconds = currentDateTime.getSeconds();

    return `${year}-${month}-${date} ${hours}:${minutes}:${seconds}`;
}