<script lang="ts">
  import type { UserTaskDto } from "../../nlp-api";

  export let task: UserTaskDto;
</script>

<div class="task">
  (#{task.taskID}) [{task.taskPriority}] {task.taskName}
</div>