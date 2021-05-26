import entities.Message;
import entities.Order;
import io.cucumber.java.en.Given;
import io.cucumber.java.en.Then;
import io.cucumber.java.en.When;
import io.restassured.http.ContentType;
import io.restassured.response.Response;
import org.hamcrest.Matchers;
import org.junit.Assert;

import java.util.Arrays;
import java.util.Comparator;
import java.util.List;

import static io.restassured.RestAssured.given;
import static org.junit.Assert.assertEquals;

public class ApiTestSteps {
    private Response response;
    private String city;
    private String url;


    @Given("city is {string}")
    public void getRequest(String city) {
        url = "https://api.openweathermap.org/data/2.5/weather?q=" + city + "&appid=1f7f0d7b3906c31e1158ca98f1fea4c2&units=metric&lang=ru";
    }

    @When("user try to get forecast by city name")
    public void getOrderById() {
        response = given()
                .contentType(ContentType.JSON)
                .when()
                .get(url)
                .then()
                .extract().response();
    }

    @Then("user receive status code {int}")
    public void compareStatusCodes(int statusCode) {
        assertEquals(statusCode, response.statusCode());
    }

    @Then("user receive message {string}")
    public void compareResponseMessage(String expectedMessage) {
        Message message = response.body().as(Message.class);
        Assert.assertEquals(expectedMessage, message.getMessage());
    }

    @Then("response don't equal zero")
    public void checkThatResponseNotNull() {
        response.then().body(Matchers.notNullValue());
    }
}
