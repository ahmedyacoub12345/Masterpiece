const url = "https://localhost:44364/api/Contact/AddMessage";
var form = document.getElementById("contact-form");
async function sendMessage() {
  var formData = new FormData(form);
  let response = await fetch(url, {
    method: "POST",
    body: formData,
  });
  var data = response;
  console.log(response);
  if (response.ok) {
    alert("Message sent successfully");
    // form.reset();
  } else {
    alert("Failed to send message");
  }
}
