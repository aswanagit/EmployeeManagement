$(document).ready(function () {
    bindEvents();
    hideEmployeeDetailCard();
});

function bindEvents() {
    $(".employeeDetails").on("click", function (event) {
        var employeeId = event.currentTarget.getAttribute("data-id");

        $.ajax({
            url: 'https://localhost:6001/api/internal/employee/' + employeeId,
            type: 'GET',
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                var newEmployeeCard = `<div class="card">
                                        
                                          <h1>${result.name}</h1>
                                         <b>Id :</b> <p>${result.id}</p>
                                         <b>Department:</b><p>${result.department}</p>
                                         <b>Age:</b><p>${result.age}</p>
                                         <b>Address:</b><p>${result.address}</p>
                                        </div>`

                $("#EmployeeCard").html(newEmployeeCard);
                showEmployeeDetailCard();
            },
            error: function (error) {
                console.log(error);
            }
        });
    });

    $(".employeeDelete").on("click", function (event) {
        
        var employeeId = event.currentTarget.getAttribute("data-id");
        var display = confirm("ARE YOU SURE WANT TO DELETE THE EMPLOYEE ?")
        if (display)
        {

            $.ajax({
                url: 'https://localhost:6001/api/internal/employee/' + employeeId,
                type: 'DELETE',
                contentType: "application/json; charset=utf-8",
                success: function (result) {

                    alert("Successfully Deleted")
                    location.reload();

                },
                error: function (error) {
                    console.log(error);
                }
            });
        }
        else
        {
            alert("DELETION CANCELLED");
        }
       
    });

    $("#createform").submit(function (event) {

        var employeeDetailedViewModel = {};

        employeeDetailedViewModel.Name = $("#name").val();
        employeeDetailedViewModel.Department = $("#dept").val();
        employeeDetailedViewModel.Age = Number($("#age").val());
        employeeDetailedViewModel.Address = $("#address").val();

        var data = JSON.stringify(employeeDetailedViewModel);

            $.ajax({
                    url: "https://localhost:6001/api/internal/employee/insert/",
                    type: "POST",
                    dataType: 'json',
                    contentType: "application/json;charset=utf-8",
                    data: data,
                    success: function (result) {

                        location.reload();
                        
                    },
                    error: function (error) {
                        console.log(error);
                    }
            });
       
    });

    $("#updateform").submit(function (event) {

        var employeeDetailedViewModel = {};

        employeeDetailedViewModel.Id = Number($("#id").val());
        employeeDetailedViewModel.Name = $("#names").val();
        employeeDetailedViewModel.Department = $("#department").val();
        employeeDetailedViewModel.Age = Number($("#ages").val());
        employeeDetailedViewModel.Address = $("#addres").val();

        var data = JSON.stringify(employeeDetailedViewModel);

        $.ajax({
            url: "https://localhost:6001/api/internal/employee/update/",
            type: "PUT",
            dataType: 'json',
            contentType: "application/json;charset=utf-8",
            data: data,
            success: function (result) {

                location.reload();

            },
            error: function (error) {
                console.log(error);
            }
        });

    });
}

 
function hideEmployeeDetailCard() {
    $("#EmployeeCard").hide();
}

function showEmployeeDetailCard() {
    $("#EmployeeCard").show();
}