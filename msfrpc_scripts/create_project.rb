#!/usr/bin/env ruby

require 'rubygems'
require 'msfrpc-client'

if ARGV.length <= 1
  p 'create_project.rb <Project Name> <remote report path> [remote report path] ...'
  p 'Example: ./create_project.rb "Mayhem" /root/nexpose.xml /root/nessus.nessus /root/nmap.xml'
else

  projectName = ARGV[0]

  opts = {}
  opts[:host] = "192.168.1.141"
  opts[:user] = "metasploit"
  opts[:pass] = "P[.=~v5Y"

  rpc = Msf::RPC::Client.new(opts)

  resp = rpc.call("console.create")
  console_id = resp["id"]

  opts = {
    "name" => "Project#{projectName}",
    "boundary" => "",
    "description" => "A project I created.",
    "limit_to_network" => true
  }

  #/var/lib/gems/1.9.1/gems/msfrpc-client-1.0.1/lib/msfrpc-client/client.rb:95:in 
  #`call': NoMethodError undefined method `id' for "root":String ["lib/pro/rpc/v10/rpc_pro.rb:54:in 
  #`rpc_workspace_add'", "lib/pro/rpc/v10/rpc_pro.rb:62:in 
  #`rpc_project_add'", "lib/msf/core/rpc/v10/service.rb:148:in 
  #`block in process'", "lib/ruby/1.9.1/timeout.rb:57:in 
  #`timeout'", "lib/msf/core/rpc/v10/service.rb:148:in 
  #`process'", "lib/msf/core/rpc/v10/service.rb:88:in 
  #`on_request_uri'", "lib/msf/core/rpc/v10/service.rb:70:in 
  #`block in start'", "lib/rex/proto/http/handler/proc.rb:37:in 
  #`call'", "lib/rex/proto/http/handler/proc.rb:37:in 
  #`on_request'", "lib/rex/proto/http/server.rb:354:in 
  #`dispatch_request'", "lib/rex/proto/http/server.rb:288:in 
  #`on_client_data'", "lib/rex/proto/http/server.rb:148:in 
  #`block in start'", "lib/rex/io/stream_server.rb:47:in 
  #`call'", "lib/rex/io/stream_server.rb:47:in 
  #`on_client_data'", "lib/rex/io/stream_server.rb:191:in 
  #`block in monitor_clients'", "lib/rex/io/stream_server.rb:189:in 
  #`each'", "lib/rex/io/stream_server.rb:189:in 
  #`monitor_clients'", "lib/rex/io/stream_server.rb:72:in 
  #`block in start'", "lib/rex/thread_factory.rb:21:in 
  #`call'", "lib/rex/thread_factory.rb:21:in 
  #`block in spawn'", "lib/msf/core/thread_manager.rb:64:in 
  #`call'", "lib/msf/core/thread_manager.rb:64:in 
  #`block in spawn'"] (Msf::RPC::ServerException)
  
  #these method calls give the above stack trace
  #resp = rpc.call("pro.project_add", opts)
  #resp = rpc.call("pro.workspace_add", opts)

  #project and workspace_add throw a method undefined error
  #workaround
  create_workspace = "workspace -a Project#{projectName}\n"
  resp = rpc.call("console.write", console_id, create_workspace)
  resp = rpc.call("console.read", console_id)

  resp = rpc.call("console.write", console_id, "workspace Project#{projectName}\n")
  resp = rpc.call("console.read", console_id)

  1.upto(ARGV.length - 1) do |arg|

    p "Importing #{ARGV[arg]}"

    #/var/lib/gems/1.9.1/gems/msfrpc-client-1.0.1/lib/msfrpc-client/client.rb:95:in 
    #`call': NoMethodError undefined method `start_module_task' for #<Pro::RPC::RPC_Pro:0x0000000b03ecf0> ["lib/pro/rpc/v10/stubs/import.rb:24:in 
    #`rpc_import_file'", "lib/msf/core/rpc/v10/service.rb:148:in 
    #`block in process'", "lib/ruby/1.9.1/timeout.rb:57:in 
    #`timeout'", "lib/msf/core/rpc/v10/service.rb:148:in 
    #`process'", "lib/msf/core/rpc/v10/service.rb:88:in 
    #`on_request_uri'", "lib/msf/core/rpc/v10/service.rb:70:in 
    #`block in start'", "lib/rex/proto/http/handler/proc.rb:37:in 
    #`call'", "lib/rex/proto/http/handler/proc.rb:37:in 
    #`on_request'", "lib/rex/proto/http/server.rb:354:in 
    #`dispatch_request'", "lib/rex/proto/http/server.rb:288:in 
    #`on_client_data'", "lib/rex/proto/http/server.rb:148:in 
    #`block in start'", "lib/rex/io/stream_server.rb:47:in 
    #`call'", "lib/rex/io/stream_server.rb:47:in 
    #`on_client_data'", "lib/rex/io/stream_server.rb:191:in 
    #`block in monitor_clients'", "lib/rex/io/stream_server.rb:189:in 
    #`each'", "lib/rex/io/stream_server.rb:189:in 
    #`monitor_clients'", "lib/rex/io/stream_server.rb:72:in 
    #`block in start'", "lib/rex/thread_factory.rb:21:in 
    #`call'", "lib/rex/thread_factory.rb:21:in 
    #`block in spawn'", "lib/msf/core/thread_manager.rb:64:in 
    #`call'", "lib/msf/core/thread_manager.rb:64:in 
    #`block in spawn'"] (Msf::RPC::ServerException)

    #commented code below throws the above exception

    #arg is a remote path, not local.
    #I mount the remote /tmp via sshfs and copy to there.
    #resp = rpc.call("pro.import_file", "Project#{projectName}", ARGV[arg], { "blacklist_hosts" => "", "preserve_hosts" => false})

    #breaks
    #file = ::File.open("/home/bperry/Downloads/report.xml", "r");
    #nexposeXML = file.read
    #resp = rpc.call("pro.import_data", "Project#{projectName}", nexposeXML, { "blacklist_hosts" => "", "preserve_hosts" => false})

    opts = {}
    opts['workspace'] = "Project#{projectName}"
    opts['username'] = 'metasploit'
    opts['DS_PATH'] = ARGV[arg]

    #start_import works as expected
    resp = rpc.call("pro.start_import", opts)
    p resp.inspect

    import_task = resp['task_id']

    done = false
    while not done
      p "Waiting on task #{import_task}"
      select(nil, nil, nil, 60)
      resp = rpc.call("pro.task_status", import_task)
      done = true if resp[import_task]['status'] == "done"
    end

    #I do this because the import api methods were breaking.
    #resp = rpc.call("console.write", console_id, "db_import #{ARGV[arg]}\n")
    #resp = rpc.call("console.read", console_id)

    resp = rpc.call("console.write", console_id, "hosts\n");
    resp = rpc.call("console.read", console_id)
    p resp["data"]
  end

  opts = {
      "workspace" => "Project#{projectName}",
      "DS_WHITELIST_HOSTS" => "192.168.1.0/24",
      "DS_MinimumRank" => "great",
      "DS_EXPLOIT_SPEED" => 5,
      "DS_EXPLOIT_TIMEOUT" => 2,
      "DS_LimitSessions" => true,
      "DS_MATCH_VULNS" => true,
      "DS_MATCH_PORTS" => true
  }

  p "Starting automagic exploitation of imported hosts..."
  resp = rpc.call("pro.start_exploit", opts)
  task = resp["task_id"]

  done = false
  while not done
    p "Waiting on task #{task}."
    select(nil, nil, nil, 60)
    resp = rpc.call("pro.task_status", task)
    done = true if resp[task]["status"] == "done"
  end

  p "Task #{task} is done. Getting report."

  opts = {
    'DS_WHITELIST_HOSTS' => "",
    'DS_BLACKLIST_HOSTS' => "",
    'workspace' => "Project#{projectName}",
    'DS_MaskPasswords' => false,
    'DS_IncludeTaskLog' => false,
    'DS_JasperDisplaySession' => true,
    'DS_JasperDisplayCharts' => true,
    'DS_LootExcludeScreenshots' => false,
    'DS_LootExcludePasswords' => false,
    'DS_JasperTemplate' => "msfxv3.jrxml",
    'DS_REPORT_TYPE' => "PDF",
    'DS_UseJasper' => true,
    'DS_UseCustomReporting' => true,
    'DS_JasperProductName' => "Metasploit Pro",
    'DS_JasperDbEnv' => "production",
    'DS_JasperLogo' => '',
    'DS_JasperDisplaySections' => "1,2,3,4,5,6,7,8",
    'DS_EnablePCIReport' => true,
    'DS_EnableFISMAReport' => true,
    'DS_JasperDisplayWeb' => true
  }

  resp = rpc.call("pro.start_report", opts)
  p resp.inspect
  report_task = resp["task_id"]

  done = false
  while not done
    p "Waiting on task #{report_task}."
    select(nil, nil, nil, 60)
    resp = rpc.call("pro.task_status", report_task)

    #"{\"54\"=>{\"status\"=>\"done\", 
    #\"error\"=>\"Module Exception: nil can't be coerced into Fixnum /pro/report.rb:142:in `+'\\n/pro/report.rb:142:in 
    #`block in run'\\n/pro/report.rb:141:in 
    #`each'\\n/pro/report.rb:141:in `run'\", 
    #\"created_at\"=>1332093136, 
    #\"progress\"=>100, 
    #\"description\"=>\"Reporting\", 
    #\"info\"=>\"\", 
    #\"workspace\"=>\"ProjectMayhem2335\", 
    #\"username\"=>\"\", 
    #\"result\"=>\"\", 
    #\"path\"=>\"/opt/metasploit-4.2.0/apps/pro/tasks/task_pro.report_54.txt\", 
    #\"size\"=>549}}"
    p resp.inspect

    done = true if resp[report_task]["status"] == "done"
  end

  #/var/lib/gems/1.9.1/gems/msfrpc-client-1.0.1/lib/msfrpc-client/client.rb:95:in 
  #`call': Msf::RPC::Exception No Report for Task ID ["lib/msf/core/rpc/v10/rpc_base.rb:15:in 
  #`error'", "lib/pro/rpc/v10/stubs/reports.rb:75:in 
  #`rpc_report_download_by_task'", "lib/msf/core/rpc/v10/service.rb:148:in 
  #`block in process'", "lib/ruby/1.9.1/timeout.rb:57:in 
  #`timeout'", "lib/msf/core/rpc/v10/service.rb:148:in 
  #`process'", "lib/msf/core/rpc/v10/service.rb:88:in 
  #`on_request_uri'", "lib/msf/core/rpc/v10/service.rb:70:in 
  #`block in start'", "lib/rex/proto/http/handler/proc.rb:37:in 
  #`call'", "lib/rex/proto/http/handler/proc.rb:37:in 
  #`on_request'", "lib/rex/proto/http/server.rb:354:in 
  #`dispatch_request'", "lib/rex/proto/http/server.rb:288:in 
  #`on_client_data'", "lib/rex/proto/http/server.rb:148:in 
  #`block in start'", "lib/rex/io/stream_server.rb:47:in 
  #`call'", "lib/rex/io/stream_server.rb:47:in 
  #`on_client_data'", "lib/rex/io/stream_server.rb:191:in 
  #`block in monitor_clients'", "lib/rex/io/stream_server.rb:189:in 
  #`each'", "lib/rex/io/stream_server.rb:189:in 
  #`monitor_clients'", "lib/rex/io/stream_server.rb:72:in 
  #`block in start'", "lib/rex/thread_factory.rb:21:in 
  #`call'", "lib/rex/thread_factory.rb:21:in 
  #`block in spawn'", "lib/msf/core/thread_manager.rb:64:in 
  #`call'", "lib/msf/core/thread_manager.rb:64:in 
  #`block in spawn'"] (Msf::RPC::ServerException)

  #This code throws the above exception.
  #However, this works with a report_task that was created in the UI.
  #Only creating the report task via the API seems to trigger this.
  failed = false
  begin
    resp = rpc.call("pro.report_download_by_task", report_task.to_i)
  rescue
    failed = true
    p "Failed!"
  end

  p resp.inspect if not failed

  #By hardcoding "2", a report task created by the UI by me, rather than the API, I reach the end.
  #resp = rpc.call("pro.report_download_by_task", "2")
  #
  #I found out which report tasks were working an which ones didn't by
  #1.upto(100).each do |i|
  #  p "Trying report task number #{i}"
  #  begin
  #    resp = rpc.call("pro.report_download_by_task", i.to_s)
  #  rescue
  #    next
  #  end
  #  p "Success!"
  #end
end
