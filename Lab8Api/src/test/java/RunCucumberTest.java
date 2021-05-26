import io.cucumber.junit.Cucumber;
import io.cucumber.junit.CucumberOptions;
import org.junit.runner.RunWith;

@RunWith(Cucumber.class)
@CucumberOptions(
        features = { "src/test/java/scenarios" },
        tags = "@ApiTests",
        plugin = { "pretty", "html:target/cucumber-report.html" },
        monochrome = true
)
public class RunCucumberTest {
}
