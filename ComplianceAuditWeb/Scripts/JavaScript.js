
    function getStateList()
    {  
        //debugger;
    var countryId = $("#countryDropDown").val();
        $.ajax({
            url: '/AddGroupCompany/GetStateList',
            type: 'POST',
            datatype: 'application/json',
            contentType: 'application/json',
            data: JSON.stringify({
                stateId: +stateId
            }),
success: function(result)
            {
        $("#stateDropDown").html("");
    $.each($.parseJSON(result), function (i, state) {
        $("#stateDropDown").append($('<option></option>').val(state.StateID).html(state.StateName));
    });

},
error: function()
            {
        alert("Whooaaa! Something went wrong..")
    },
});
    } 