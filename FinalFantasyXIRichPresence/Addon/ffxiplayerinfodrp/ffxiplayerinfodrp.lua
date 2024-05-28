--[[
Copyright © 2024,1UP
All rights reserved.
Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:
    * Redistributions of source code must retain the above copyright
      notice, this list of conditions and the following disclaimer.
    * Redistributions in binary form must reproduce the above copyright
      notice, this list of conditions and the following disclaimer in the
      documentation and/or other materials provided with the distribution.
    * Neither the name of enemybar nor the
      names of its contributors may be used to endorse or promote products
      derived from this software without specific prior written permission.
THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL Mike McKee BE LIABLE FOR ANY
DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
--]]


_addon.name    = 'ffxiplayerinfodrp'
_addon.author  = '1UP'
_addon.version = '1.0'
require('tables')
local file  = require('files')
local json = require "json"
local player_table = {}
local self_storage  = file.new('data\\data.json')

function get_player_data()
    if windower.ffxi.get_info().logged_in then
		local player = windower.ffxi.get_player()
		local info = windower.ffxi.get_info()
		local party = windower.ffxi.get_party()			
		player_table =
		{
			name = player.name,
			main_jobId = player.main_job_id,
			main_job_level = player.main_job_level,
			sub_jobId = player.sub_job_id,
			sub_job_level = player.sub_job_level,
			zone_id = info.zone,
			server_id = info.server,
			party_count = party.party1_count
		}
		data_to_write = json.encode(player_table)
		self_storage:write(data_to_write)
    end
end

function did_data_change(player_data)
    local player = windower.ffxi.get_player()
    local info = windower.ffxi.get_info()
    local party = windower.ffxi.get_party()	

    if player_data["name"] ~= player.name then
        return true
    end
    
    if player_data["main_jobId"] ~= player.main_job_id then
        return true
    end

    if player_data["main_job_level"] ~= player.main_job_level then
        return true
    end

    if player_data["sub_jobId"] ~= player.sub_job_id then
        return true
    end    

    if player_data["sub_job_level"] ~= player.sub_job_level then
        return true
    end    

    if player_data["zone_id"] ~= info.zone then
        return true
    end    

    if player_data["server_id"] ~= info.server then
        return true
    end        

    if player_data["party_count"] ~= party.party1_count then
        return true
    end        
    
    return false
end

function time_changed(new,old)
	if did_data_change(player_table) then
		windower.console.write("Data updated")
		get_player_data()
	end
end

function unload()
		player_table =
		{
			name = "N/a",
			main_jobId = -1,
			main_job_level = -1,
			sub_jobId = -1,
			sub_job_level = -1,
			zone_id = -1,
			server_id = -1,
			party_count = -1
		}
		data_to_write = json.encode(player_table)
		self_storage:write(data_to_write)
end

windower.register_event('time change', time_changed)
windower.register_event('load', get_player_data)
windower.register_event('unload', unload)

--[[
windower.register_event('addon command', function(command1, ...)
	get_player_data()
end)]]--