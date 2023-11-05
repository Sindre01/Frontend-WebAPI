<script>
  import { Wave } from 'svelte-loading-spinners';

  export let loading;
  export let resultText;
  import { fly, fade } from 'svelte/transition';
  import { cubicInOut } from 'svelte/easing';
</script>

<div class = "result">
      {#if loading}
        <p>Making data ready</p> 
        <Wave size="20" color="#ee610f" unit="px" duration="1s" />
      {:else}
          <p in:fly={{ y: -100, duration: 800, easing: cubicInOut }}>{resultText}</p>
      {/if}
  </div>

  <style>
    .result{
      display: flex;
      flex-direction: row;
      align-items: center;
      margin-top: 5em;
      justify-content: space-evenly;
    }
  </style>