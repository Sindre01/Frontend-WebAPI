<script>

  import confetti from 'canvas-confetti';
  import Result from './lib/Result.svelte';

  let topic = "";
  let resultText = "";
  let loading = false;
  
  //Reset confetti AND resulttext when topic changes
  $: if (topic != null) {
    confetti.reset();
    resultText = "";
  }
  
  const handleSubmit = () => {
    if (topic === "") {
        resultText = 'Please enter a topic.';
        return;
    }
    getTopicCount();   
  }

  async function getTopicCount() { //async function to fetch data from backend api
      try {
          loading = true;
          //Filtering out special characters with encodeURIComponent
          let url = "https://localhost:4000/api/wikipedia/"+ encodeURIComponent(topic);

          const response = await fetch(url);
          if (response.ok) {
              const result = await response.json();
              resultText = "The topic '" + result.topic +"' appears " + result.count + " times.";
             
              if (result.count > 0) {
                confetti({
                  particleCount: 150,
                  spread: 100,
                });
              }
            
          } else {
              throw new Error('Error fetching data.');
          }
      } catch (error) {
          resultText = "Could not establish connection to API";
          
      } finally {
          loading = false;
      }
  }
</script>

<main>
  <h1>Wikipedia Topic Counter</h1>
  <div class ="container">
    <form on:submit|preventDefault={handleSubmit}>
      <input type="text" bind:value={topic} placeholder="Enter a Wikipedia topic" />
      <button type="submit">Find occurences</button>
    </form>
    <Result {resultText} {loading} />
  </div> 
</main>

<style>
  main {
      text-align: center;
  }
  .container {
    text-align: center;
      padding: 1em;
      max-width: 240px;
      margin: 0 auto;
  }

  input[type="text"] {
      width: 100%;
      padding: 0.5em;
  }
  button {
      width: 100%;
      padding: 0.5em;
      margin-top: 0.5em;
  }

</style>
